# USAGE
# make_version.py target_version_name [branch_name]
# branch_name can be eg. 2.3 (folder name from branches folder), of nothing is given, trunk is assumed

import os, stat, zipfile, shutil, ftplib, string, time, sys, datetime
from cStringIO import StringIO
from lxml import etree

from build_defs import *

def doupload(host, user, password, root):
    print "Provadim upload na %s" % host
    ftp = ftplib.FTP(host)
    ftp.login(user = user, passwd = password)
    
    if issnapshot or istag:
        fr = open("install/datadmin-install.exe", "rb")
        ftp.storbinary("STOR %s/datadmin-%s.exe" % (root, version), fr)
        
#         fr = open("install/datadmin-linux.tgz", "rb")
#         ftp.storbinary("STOR %s/datadmin-%s.tgz" % (root, version), fr)
#         
#         fr = open("install/datadmin-debian.deb", "rb")
#         ftp.storbinary("STOR %s/datadmin-%s.deb" % (root, version), fr)
    elif isbeta:
        fr = open("install/datadmin-install.exe", "rb")
        ftp.storbinary("STOR %s/datadmin-beta.exe" % root, fr)
        
#         fr = open("install/datadmin-linux.tgz", "rb")
#         ftp.storbinary("STOR %s/datadmin-beta.tgz" % root, fr)
#         
#         fr = open("install/datadmin-debian.deb", "rb")
#         ftp.storbinary("STOR %s/datadmin-beta.deb" % root, fr)
    else:
        fr = open("install/datadmin-install.exe", "rb")
        ftp.storbinary("STOR %s/datadmin-install.exe" % root, fr)

        fr = open("install/datadmin-install.exe", "rb")
        ftp.storbinary("STOR %s/datadmin2-install.exe" % root, fr)

#         fr = open("install/datadmin-linux.tgz", "rb")
#         ftp.storbinary("STOR %s/datadmin-linux.tgz" % root, fr)
# 
#         fr = open("install/datadmin-debian.deb", "rb")
#         ftp.storbinary("STOR %s/datadmin-linux.deb" % root, fr)
    
        if not isfix:
            fr = open("install/datadmin-pad.xml", "rb")
            ftp.storbinary("STOR %s/datadmin-pad.xml" % root, fr)
    
            fr = open("install/datadmin-pro-pad.xml", "rb")
            ftp.storbinary("STOR %s/datadmin-pro-pad.xml" % root, fr)
    
            fr = open("install/datadmin-versiondb-pad.xml", "rb")
            ftp.storbinary("STOR %s/datadmin-versiondb-pad.xml" % root, fr)
    
    ftp.quit()

def svninfo(path):
    fr = os.popen('svn info %s' % path, 'r')
    res = {}
    for line in fr:
        try:
            name, value = line.strip().split(':', 1)
            res[name.strip()] = value.strip()
        except:
            pass
    return res

def modify_version_file(fn, brand):
    print 'Modifying', fn
    vinfo = open(fn).read()
    vinfo = vinfo.replace('#BUILT_AT#', datetime.datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%S"))
    vinfo = vinfo.replace('#SVN_REVISION#', svninfo('.')['Revision'])
    vinfo = vinfo.replace('#VERSION#', version)
    vinfo = vinfo.replace('#PRGTITLE#', brand.ProgramTitle)
    vinfo = vinfo.replace('#PRGFOLDER#', brand.ProgramFolder)
    vinfo = vinfo.replace('#LICINFO#', brand.LicenseInfo.replace("\"", "\\\"").replace('\r', '').replace('\n', '\\n'))
    vinfo = vinfo.replace('#HIDELICINFO#', brand.HideLicenseInfo)
    vinfo = vinfo.replace('#HIDEPURCHASELINKS#', brand.HidePurchaseLinks)
    vinfo = vinfo.replace('#DENYCUSTOMLICENSES#', brand.DenyCustomLicenses)
    vinfo = vinfo.replace('#PLUGINFILTER#', brand.PluginFilter)
    vinfo = vinfo.replace('#HIDEGROUPSINCREATEDIALOG#', brand.HideGroupsInCreateDialog)
    vinfo = vinfo.replace('#DATABASESAMPLESFOLDER#', brand.DatabaseSamplesFolder)
    vinfo = vinfo.replace('#ALLOWAUTOUPDATE#', brand.AllowAutoUpdate)
    vinfo = vinfo.replace('#BRAND#', brand.BrandName)

    open(fn, 'wb').write(vinfo)


def copyfiles(srcfolder, dstfolder, files):
    for f in files:
        shutil.copyfile(os.path.join(srcfolder, f), os.path.join(dstfolder, f))

def copyallfiles(srcfolder, dstfolder):
    for f in os.listdir(srcfolder):
        if not os.path.isfile(os.path.join(srcfolder, f)): continue
        shutil.copyfile(os.path.join(srcfolder, f), os.path.join(dstfolder, f))

def isplatform(xml, platform):
    if xml is None: return True
    if not xml.attrib.has_key('platforms'): return True
    return platform in xml.attrib['platforms']

def isbrand(xml, brand):
    if xml is None: return True
    if brand == 'default': return True
    if xml.attrib.has_key(brand): return xml.attrib[brand] == '1'
    return True

def add_directory(dparent, relpath, extxml, brand):
    global create_content_nsi, delete_content_nsi, ziparch
    if not isbrand(dparent, brand) or not isbrand(extxml, brand): return
    if isplatform(dparent, 'windows') and isplatform(extxml, 'windows'):
        create_content_nsi += '  CreateDirectory "%s\\%s"\n' % (WINROOT[dparent.attrib['parent']], relpath.replace('/', '\\')) 
        delete_content_nsi = '  RMDir "%s\\%s"\n' % (WINROOT[dparent.attrib['parent']], relpath.replace('/', '\\')) + delete_content_nsi 
    for pl in UNIX_PLATFORMS:
        if isplatform(dparent, pl) and isplatform(extxml, pl):
            dirname = os.path.join('datadmin-%s' % pl, LINROOT[dparent.attrib['parent']], relpath)
            if not os.path.isdir(dirname): 
                os.makedirs(dirname)
    dirname = os.path.join('archive', dparent.attrib['parent'], relpath)
    if not os.path.isdir(dirname):
        os.makedirs(dirname)

def add_file(dparent, fxml, relpath, fname, src, brand):
    global create_content_nsi, delete_content_nsi, ziparch
    if not isbrand(dparent, brand) or not isbrand(fxml, brand): return
    if isplatform(dparent, 'windows') and isplatform(fxml, 'windows'):
        create_content_nsi += ' SetOutPath "%s"\n' % os.path.join(WINROOT[dparent.attrib['parent']], relpath).replace('/', '\\')
        create_content_nsi += ' File "%s"\n' % src.replace('#BIN#', r'DatAdmin\bin\Release').replace('/', '\\') 
        delete_content_nsi = ' Delete "%s"\n' % os.path.join(WINROOT[dparent.attrib['parent']], relpath, fname).replace('/', '\\') + delete_content_nsi
    for pl in UNIX_PLATFORMS:
        if isplatform(dparent, pl) and isplatform(fxml, pl):
            shutil.copy(src.replace("#BIN#", r'DatAdmin\bin\Release'), os.path.join('datadmin-%s' % pl, LINROOT[dparent.attrib['parent']], relpath))
            
    shutil.copy(src.replace("#BIN#", r'DatAdmin\bin\Release'), os.path.join('archive', dparent.attrib['parent'], relpath))
    
    # allow to filter only selecter root directories to archive in ZIP
    bobj = loadbrand(brand)
    if bobj.RootFilter is None: 
        ziparch.write(src.replace("#BIN#", r'DatAdmin\bin\Release'), os.path.join(dparent.attrib['parent'], relpath, fname).replace('\\', '/'))
    else:
        if bobj.RootFilter == dparent.attrib['parent']:
            ziparch.write(src.replace("#BIN#", r'DatAdmin\bin\Release'), os.path.join(relpath, fname).replace('\\', '/'))

def add_content(dparent, srcfolder, dstfolder, item, brand):  
    #if isplatform(dparent, 'windows') and isplatform(item, 'windows'):
#        srcdir = 
    for f in os.listdir(srcfolder):
        if os.path.isfile(os.path.join(srcfolder, f)):
            add_file(dparent, None, dstfolder, f, os.path.join(srcfolder, f), brand)
        if os.path.isdir(os.path.join(srcfolder, f)):
            if '.svn' in f.lower():
                continue
            add_directory(dparent, os.path.join(dstfolder, f), item, brand)
            add_content(dparent, os.path.join(srcfolder, f), os.path.join(dstfolder, f), item, brand)

def prepare_pad(padname):
    padtpl = open('install/' + padname + '-template.xml').read()
    padtpl = padtpl.replace('#VERSION#', version)
    
    padtpl = padtpl.replace('#MONTH#', time.strftime('%m'))
    padtpl = padtpl.replace('#DAY#', time.strftime('%d'))
    padtpl = padtpl.replace('#YEAR#', time.strftime('%Y'))
    
    sizebytes = os.path.getsize('install/datadmin-install.exe')
    padtpl = padtpl.replace('#SIZE_BYTES#', str(sizebytes))
    padtpl = padtpl.replace('#SIZE_KBYTES#', str(sizebytes/1024))
    padtpl = padtpl.replace('#SIZE_MBYTES#', '%0.2f' % (sizebytes/1024.0/1024.0))
    open('install/' + padname + '.xml', 'w').write(padtpl)

def create_installations(brand):
    global create_content_nsi, delete_content_nsi, ziparch
    
    ziparch = zipfile.ZipFile('archive.zip', 'w', zipfile.ZIP_DEFLATED)
    print 'Creating installations...'
    os.chdir("install")
    # directory for linux distribution
    for pl in UNIX_PLATFORMS:
        os.makedirs('datadmin-%s/usr/lib/datadmin' % pl) 
        os.makedirs('datadmin-%s/etc/datadmin/appdata' % pl)
    os.mkdir('archive')     
    os.mkdir('archive/install')
    os.mkdir('archive/appdata')
    doc = etree.parse(open('install.xml'))
    create_content_nsi = ''
    delete_content_nsi = ''
    for dparent in doc.xpath('/Install/Directory'):
        try: relpath = dparent.attrib['path']
        except: relpath = ''
        if relpath: add_directory(dparent, relpath, None, brand)
        for item in dparent:
            if item.tag == 'File':
                add_file(dparent, item, relpath, item.attrib['name'], os.path.join('..', item.attrib['src']), brand)
            if item.tag == 'CopyAll' and isbrand(item, brand):
                add_content(dparent, os.path.join("..", item.attrib['src']), relpath, item, brand)
    
    data = open('install-tpl.nsi').read()
    data = data.replace('#CREATE_CONTENT#', create_content_nsi)
    data = data.replace('#DELETE_CONTENT#', delete_content_nsi)
    data = data.replace('#VERTYPE#', vertype)
    data = data.replace('#SPACEVERTYPE#', spvertype)
    open('install-repl.nsi', 'w').write(data)
    
    print 'Creating windows installer...'
    os.system('"%s" install-repl.nsi' % MAKENSIS)
    # signcode
    os.system(r'"%s" sign /f ..\doc\GlobalSign_cert.p12 /p draklesu datadmin-install.exe' % SIGNTOOL)
    
    # print 'Creating linux distributions...'
    # os.system('tar -cv --file=datadmin-linux.tar datadmin-linux/*')
    # os.system('gzip -9 < datadmin-linux.tar > datadmin-linux.tgz')
    # 
    # os.system('tar -cv --file=datadmin-debian.tar datadmin-debian/*')
    # os.system('gzip -9 < datadmin-debian.tar > datadmin-debian.tgz')
    # 
    # print 'Copying to linux build machine...'
    # os.system('ssh jena@jdesktop "rm -rf .dinst"') 
    # os.system('ssh jena@jdesktop "mkdir .dinst"') 
    # os.system('ssh jena@jdesktop "cat > .dinst/datadmin-debian.tgz" < datadmin-debian.tgz')
    # print 'Remote compiling debian distribution...'
    # data = open('compile_linux.sh', 'r').read()
    # open('compile_linux.sh', 'wb').write(data)
    # os.system('ssh jena@jdesktop < compile_linux.sh') 
    # print 'Download debian distribution...'
    # os.system('ssh jena@jdesktop "cat .dinst/datadmin-debian.deb" > datadmin-debian.deb') 
    
    os.chdir("..")
    ziparch.close()


version = sys.argv[1]
istag = False
if version.startswith('tag:'):
    version = version[4:]
    istag = True 
issnapshot = '.rev' in version
isbeta = not issnapshot and (int(version.split('.')[1]) % 2) == 1 # druhe cislo ve verzi je liche
isrelease = not issnapshot and (int(version.split('.')[1]) % 2) == 0 # druhe cislo ve verzi je sude
isfix = len(version.split('.')) == 4 and not issnapshot

if issnapshot:
    if (int(version.split('.')[1]) % 2) == 1: # druhe cislo ve verzi je liche
        vertype = 'ALPHA' 
    if (int(version.split('.')[1]) % 2) == 0: # druhe cislo ve verzi je sude
        vertype = 'GAMMA' 
elif isbeta:
    vertype = 'BETA'
else:
    vertype = ''

print 'Building', vertype,
if isfix: print 'FIX'
if istag: print 'TAG', version
print

if len(vertype) > 0: spvertype = ' ' + vertype
else: spvertype = vertype  

try: branch = 'branches/' + sys.argv[2]
except: branch = 'trunk'

if istag:
    branch = '/tags/' + version

emptydir(".bld")
    
emptydir(".version")
if not os.path.isdir('.bld') : os.mkdir(".bld")

os.chdir(".bld")

print 'Checkouting from SVN...'
os.system("svn co %s/%s datadmin" % (SVNBASE, branch))

os.chdir("datadmin")
os.chdir("DatAdmin")

if '.rev' in version:
    version = version.replace('rev', svninfo('.')['Revision'])  

defbrand = loadbrand('default')
modify_version_file('DatAdmin.Framework/VersionInfo.cs', defbrand)
modify_version_file('DatAdmin/SplashForm.Designer.cs', defbrand)
modify_version_file('install/debian-root/DEBIAN/control', defbrand)

print 'Building DatAdmin...'
os.system(r'"%s" DatAdmin.sln /Build Release' % DEVENV)
#os.system(r'"%s" DatAdmin2.sln /Build Debug' % DEVENV)
#print 'Exporting packages...'
#os.system(r'DatAdmin\bin\Debug\daci.exe exportaddons alladdons.adp --dir addons') # addons are in debug directory

create_installations('default')

# CREATE PAD FILES
prepare_pad('datadmin-pad')
prepare_pad('datadmin-versiondb-pad')
prepare_pad('datadmin-pro-pad')

if issnapshot or istag:
    doupload(FTPHOST, FTPLOGIN, FTPPASSWORD, '/web/datadmin.com/snapshot')
elif isbeta:
    doupload(FTPHOST, FTPLOGIN, FTPPASSWORD, '/web/datadmin.com')
else: # release
    doupload(FTPHOST, FTPLOGIN, FTPPASSWORD, '/web/datadmin.com')
    doupload(FTPHOST2, FTPLOGIN2, FTPPASSWORD2, '')

print 'Uploading version...'
ftp = ftplib.FTP(FTPHOST)
ftp.login(user = FTPLOGIN, passwd = FTPPASSWORD)
if isbeta and not isfix and not istag:
    ftp.storbinary("STOR /web/datadmin.com/includes/datadmin/version-beta.php", StringIO('<?$ver_betaversion = "%s"; $ver_betachanged = "%s"; $ver_betafilename = "datadmin-beta.exe"; $ver_betafilename_tgz = "datadmin-beta.tgz"; $ver_betafilename_deb = "datadmin-beta.deb";?>' % (version, datetime.datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%S")) ))
if isrelease and not isfix and not istag:
    ftp.storbinary("STOR /web/datadmin.com/includes/datadmin/version.php", StringIO('<?$ver_version = "%s"; $ver_lastchanged = "%s";?>' % (version, datetime.datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%S")) ))
    
if not issnapshot and not isfix and not istag:
    print 'Uploading changelog...'
    os.system("svn co %s/changelog changelog" % SVNBASE)
    ftp.storbinary("STOR /web/datadmin.com/changelog-release.txt", open('changelog/release.txt'))
    ftp.storbinary("STOR /web/datadmin.com/changelog-beta.txt", open('changelog/beta.txt'))
    
ftp.quit()
ftp = ftplib.FTP(FTPHOST2)
ftp.login(user = FTPLOGIN2, passwd = FTPPASSWORD2)

if isbeta and not isfix and not istag:
    ftp.storbinary("STOR includes/datadmin/version-beta.php", StringIO('<?$ver_betaversion = "%s"; $ver_betachanged = "%s"; $ver_betafilename = "datadmin-beta.exe"; $ver_betafilename_tgz = "datadmin-beta.tgz"; $ver_betafilename_deb = "datadmin-beta.deb";?>' % (version, datetime.datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%S")) ))
if isrelease and not isfix and not istag:
    ftp.storbinary("STOR includes/datadmin/version.php", StringIO('<?$ver_version = "%s"; $ver_lastchanged = "%s";?>' % (version, datetime.datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%S")) ))

ftp.quit()
   
if not issnapshot and not istag:
    print 'Creating tag in SVN...'
    cmd = 'svn copy %s/%s %s/tags/%s -m "Creating tag %s"' % (SVNBASE, branch, SVNBASE, version, version)
    print cmd
    os.system(cmd)

os.chdir("../..")

if not istag:
    for brand in BRANDS:
        print 'Creating brand:', brand, '...'
        print 'Checkouting from SVN...'
        os.system("svn co %s/%s datadmin-%s" % (SVNBASE, branch, brand))
        
        bobj = loadbrand(brand)
    
        os.chdir("datadmin-%s" % brand)
        os.chdir("DatAdmin")
        
        modify_version_file('DatAdmin.Framework/VersionInfo.cs', bobj)
        modify_version_file('DatAdmin/SplashForm.Designer.cs', bobj)
        modify_version_file('install/debian-root/DEBIAN/control', bobj)
        if bobj.LicenseOverride is not None:
            open('DatAdmin.Core/Resources/data/personal.xml', 'w').write(bobj.LicenseOverride)
        if bobj.MainWinIcon is not None:
            open('DatAdmin/Resources/mainicon.png', 'wb').write(open(bobj.MainWinIcon, 'rb').read())
        if bobj.ProgramIcon is not None:
            open('DatAdmin/datadmin.ico', 'wb').write(open(bobj.ProgramIcon, 'rb').read())
       
        os.system(r'"%s" DatAdmin.sln /Build Release' % DEVENV)
        
        create_installations(brand)
        #os.system('tar -cv --file=datadmin-%s.tar archive/*' % brand)
        #os.system('gzip -9 < datadmin-%s.tar > datadmin-%s.tgz' % (brand, brand))
        
        print 'Uploading brand...'
        ftp = ftplib.FTP(FTPHOST)
        ftp.login(user = FTPLOGIN, passwd = FTPPASSWORD)
        fr = open("archive.zip", "rb")
        ftp.storbinary("STOR /web/datadmin.com/brands/%s/%s.zip" % (brand, version), fr)
        ftp.quit()
        
        os.chdir("../..")

os.chdir('..') # .bld

#emptydir(".bld")

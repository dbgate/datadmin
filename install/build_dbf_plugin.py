import sys, os, zipfile, os.path, ftplib, StringIO
from lxml import etree

from build_defs import *

INSXML = """<ROOT>
    <Reference Include="DatAdmin.Core, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>C:\\Program Files\\DatAdmin\\DatAdmin.Core.dll</HintPath>
    </Reference>
    <Reference Include="DatAdmin.Framework, Version=1.0.0.0, Culture=neutral, processorArchitecture=MSIL">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>C:\\Program Files\\DatAdmin\\DatAdmin.Framework.dll</HintPath>
    </Reference>
</ROOT>"""


def remove_node(xpath):
    global doc, names
    
    node = doc.xpath(xpath, namespaces=names)[0]
    node.getparent().remove(node)


try: branch = 'branches/' + sys.argv[1]
except: branch = 'trunk'

emptydir(".bld-plug")
    
emptydir(".version")
if not os.path.isdir('.bld-plug') : os.mkdir(".bld-plug")

os.chdir(".bld-plug")

os.mkdir('DBFPlugin')
os.chdir("DBFPlugin")

print 'Checkouting from SVN...'
os.system("svn export %s/%s/DatAdmin/DBFPlugin.sln" % (SVNBASE, branch))
os.system("svn export %s/%s/DatAdmin/Plugin.dbf" % (SVNBASE, branch))
os.system("svn export %s/%s/DatAdmin/FastDBF" % (SVNBASE, branch))


print 'Change CSPROJ file'
doc = etree.parse(open('Plugin.dbf/Plugin.dbf.csproj'))
names = {'x': 'http://schemas.microsoft.com/developer/msbuild/2003'}

remove_node("//x:ProjectReference[x:Name='DatAdmin.Core']")
remove_node("//x:ProjectReference[x:Name='DatAdmin.Framework']")

node = doc.xpath("//x:ItemGroup", namespaces=names)[0]
insxml = etree.parse(StringIO.StringIO(INSXML))
for child in insxml.getroot():
    node.append(child)

for op in doc.xpath("//x:OutputPath", namespaces=names):
    op.text = "C:\\Program Files\\DatAdmin\\"

doc.write(open('Plugin.dbf/Plugin.dbf.csproj', 'w'))       

os.chdir("..")


print 'Creating archive...'

os.system('..\\tar -cv --file=DBFPlugin.tar DBFPlugin/*')
os.system('..\\gzip -9 < DBFPlugin.tar > DBFPlugin.tgz')

print 'Uploading to datadmin.com'

ftp = ftplib.FTP(FTPHOST)
ftp.login(user = FTPLOGIN, passwd = FTPPASSWORD)
fr = open("DBFPlugin.tgz", "rb")
ftp.storbinary("STOR %s/DBFPlugin.tgz" % '/web/datadmin.com', fr)
ftp.close()

print 'Build finished'

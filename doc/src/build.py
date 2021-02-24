import os, os.path, sys, shutil, re
from lxml import etree

def copy_lang_file(srcfile, dstfile, lang):
    open(('../html/' + dstfile).replace('#LANG#', lang), 'w').write(open(srcfile, 'r').read().replace('#LANG#', lang))

def process_index(lang):
    doc = etree.parse(open('index-%s.xml' % lang))
    for x in doc.xpath('//item'):
        if x.attrib.has_key('href'):
            fnl = '../html/%s-%s.html' % (x.attrib['href'], lang) 
            fne = '../html/%s-%s.html' % (x.attrib['href'], 'en')
            if os.path.isfile(fnl): x.attrib['href'] = '%s-%s.html' % (x.attrib['href'], lang)
            else: x.attrib['href'] = '%s-%s.html' % (x.attrib['href'], 'en')
    xml2hhc.apply(doc).write(open('../html/index-%s.hhc' % lang, 'w'))
    xml2html_index.apply(doc).write(open('../html/content-%s.html' % lang, 'w'))
    copy_lang_file('index-frames.html', 'index-#LANG#.html', lang)
    copy_lang_file('toolbar.html', 'toolbar-#LANG#.html', lang)

xml2html = etree.XSLT(etree.parse(open('xml2html.xslt')))
xml2hhc = etree.XSLT(etree.parse(open('xml2hhc.xslt')))
xml2html_index = etree.XSLT(etree.parse(open('xml2html_index.xslt')))

for icfn in os.listdir('../../DatAdmin.Core/Resources'):
    if not icfn.lower().endswith('.png'): continue
    shutil.copyfile(os.path.join('../../DatAdmin.Core/Resources', icfn), os.path.join('../html/img', icfn)) 

for xfn in os.listdir('.'):
    xfn = xfn.lower()
    if xfn.endswith('.xml') and not xfn.startswith('index-'):
        lang = re.search(r'-(..)\.xml', xfn)
        print 'Processing file:', xfn
        doc = etree.parse(xfn)
        doc = xml2html.apply(doc)
        xfnout = '../html/' + xfn.replace('.xml', '.html')
        doc.write(open(xfnout, 'w'))

process_index('cz')
process_index('en')

shutil.copyfile('datadmin-cz.hhp', '../html/datadmin-cz.hhp')
shutil.copyfile('datadmin-en.hhp', '../html/datadmin-en.hhp')

os.chdir(r'..\html')
os.system(r'"c:\Program Files\HTML Help Workshop\hhc" datadmin-cz.hhp')
os.system(r'"c:\Program Files\HTML Help Workshop\hhc" datadmin-en.hhp')

shutil.copyfile('datadmin-cz.chm', '../datadmin-cz.chm')
shutil.copyfile('datadmin-en.chm', '../datadmin-en.chm')

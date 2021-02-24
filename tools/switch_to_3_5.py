import os, sys
from lxml import etree

names = {'x': 'http://schemas.microsoft.com/developer/msbuild/2003'}

for root, dirs, files in os.walk('..'):
    if '.svn' in dirs:
        dirs.remove('.svn')
    if '.bld' in dirs:
        dirs.remove('.bld')
    if 'Plugin.apps' in dirs:
        dirs.remove('Plugin.apps')

    for name in files:
        fn = os.path.join(root, name)
        if fn.endswith('.csproj'):
            print >>sys.stderr, 'Processing file:', fn, '...',
            doc = etree.parse(open(fn))
            
            try:
                tar = doc.xpath('//x:TargetFrameworkVersion', namespaces=names)
                if len(tar) == 0:
                    pg = doc.xpath('//x:PropertyGroup', namespaces=names)[0]
                    tar = [etree.SubElement(pg, 'TargetFrameworkVersion')]
                
                tar[0].text = 'v3.5'
                for lb in doc.xpath('//x:Reference', namespaces=names):
                    if lb.attrib['Include'].startswith('LinqBridge'):
                        lb.getparent().remove(lb)

                inc = etree.SubElement(doc.xpath('//x:Reference[@Include="System"]', namespaces=names)[0].getparent(), 'Reference')
                inc.attrib['Include'] = 'System.Core'
                rf = etree.SubElement(inc, 'RequiredTargetFramework')
                rf.text = '3.5'
                doc.write(open(fn, 'w'))
            except Exception, e:
                print 'SKIPPED'
                continue                
            print 'OK'
            

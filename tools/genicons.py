import sys
from lxml import etree

doc = etree.parse(open(sys.argv[1]))

fw = open(sys.argv[2], 'w')

print >>fw, """using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace """ + sys.argv[3] + """
{
    public class """ + sys.argv[4] + """
    {
        public static readonly Dictionary<string, Bitmap> IconTable = new Dictionary<string, Bitmap>(); 
"""

for icon in doc.xpath('/root/data'):
    print >>fw, '%sstatic Bitmap m_%s = %s.%s;' % (" " * 8, icon.attrib['name'], sys.argv[5], icon.attrib['name'])
    
print >>fw
print >>fw, "%sstatic %s() {" % (" " * 8, sys.argv[4])
for icon in doc.xpath('/root/data'):
    print >>fw, '%sIconTable["%s"] = m_%s;' % (" " * 12, icon.attrib['name'], icon.attrib['name'])
print >>fw, "%s}" % (" " * 8)


for icon in doc.xpath('/root/data'):
    print >>fw, '%spublic static Bitmap %s {get {return m_%s;} }' % (" " * 8, icon.attrib['name'], icon.attrib['name'])

for icon in doc.xpath('/root/data'):
    print >>fw, '%spublic const string %sName = "%s";' % (" " * 8, icon.attrib['name'], icon.attrib['name'])

print >>fw, """    }
}
"""

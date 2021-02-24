import os, os.path, sys
from lxml import etree
import copy

def replace_all(s, defs):
    for d in defs:
        s = s.replace(d, defs[d])
    return s

# def process_code(xml, dstxml, defs):
#     etree.SubElement(dstxml, 'Code').text = replace_all(xml.find('Code').text, defs)
# 
# def process_runsql(xml, dstxml, defs):
#     process_code(xml, dstxml, defs)
# 
# def process_showsql(xml, dstxml, defs):
#     process_code(xml, dstxml, defs)
# 
# def process_dblist(xml, dstxml, defs):
#     process_code(xml, dstxml, defs)
#     if xml.find('ChildTitleColumn') != None:
#         etree.SubElement(dstxml, 'ChildTitleColumn').text = xml.find('ChildTitleColumn').text
#     if 'image' in xml.attrib: dstxml.attrib['image'] = xml.attrib['image'] 
#     if 'view' in xml.attrib: dstxml.attrib['view'] = xml.attrib['view']
#     if 'nodegen' in xml.attrib: dstxml.attrib['nodegen'] = xml.attrib['nodegen']
#     if 'dbentity' in xml.attrib: dstxml.attrib['dbentity'] = xml.attrib['dbentity']
    

OBJ_TYPES = {
    'RunSql': ('runsql.xsl', 'Template'),
    'DbList': ('dblist.xsl', 'Template'),
    'ShowSql': ('showsql.xsl', 'Python'),
    'GenSql': ('gensql.xsl', 'Template'),
}
NODE_TYPE_MAP = {
    'table': 'DatAdmin.Table_SourceTreeNode, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
    'database': 'DatAdmin.IDatabaseTreeNode, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
    'table': 'DatAdmin.Table_SourceTreeNode, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
    'column_list': 'DatAdmin.Columns_TreeNode, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
    'database_list': 'DatAdmin.DatabasesTreeNode, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
    'dbserver': 'DatAdmin.Server_SourceConnectionTreeNode, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
    'table_list': 'DatAdmin.Tables_TreeNode, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
    'column': 'DatAdmin.Column_TreeNode, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
    'constraint_list': 'DatAdmin.Constraints_TreeNode, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
    'constraint': 'DatAdmin.Constraint_TreeNode, DatAdmin.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
}

custom_conds = etree.parse(open('custom_conditions.xml'))

def process_tag(objx, conds, defs, res):
    tran = etree.Element('Addon')
    for elem in conds:
        tran.append(copy.deepcopy(elem))
    for elem in objx.findall('InParam'):
        tran.append(copy.deepcopy(elem))
    nodes = objx.attrib['nodes']
    ncitems = []
    for node in nodes.split('|'):
        if node in NODE_TYPE_MAP: 
            ncitems.append(NODE_TYPE_MAP[node])
        else:
            ccl = custom_conds.xpath('/CustomConditions/PropertyCondition[@refname="%s"]' % node)
            if not ccl: raise Exception('Node type not defined:' + node)  
            cc = ccl[0]
            tran.append(copy.deepcopy(cc))
    etree.SubElement(tran, 'NodeTypeConditionsText').text = '|'.join(ncitems)
    etree.SubElement(tran, 'Code').text = replace_all(objx.find('Code').text, defs)
    if 'position' not in objx.attrib: tran.attrib['position'] = '10'
    if objx.find('ChildTitleColumn') != None:
        etree.SubElement(tran, 'ChildTitleColumn').text = objx.find('ChildTitleColumn').text
    
    tran.attrib['title'] = objx.attrib['title']
    for attr in objx.attrib:
        tran.attrib[attr] = objx.attrib[attr]

    if 'lang' in objx.attrib: lang = objx.attrib['lang']
    else: lang = OBJ_TYPES[objx.tag][1] 
    tran.attrib['lang'] = lang 
    
    #OBJ_TYPES[objx.tag][1](objx, tran, defs)
    
    xsltdoc = etree.parse(OBJ_TYPES[objx.tag][0])
    xsltdoc.xinclude()
    xslt = etree.XSLT(xsltdoc)
    transformed = xslt.apply(etree.ElementTree(tran))
    for item in transformed.getroot():
        res.append(copy.deepcopy(item))

def process_tags(root, conds, defs, res):
    for objx in root:
        if objx.tag in OBJ_TYPES:
            process_tag(objx, conds, defs, res)
        elif objx.tag == 'Use':
            doc = etree.parse(open(objx.attrib['module']))
            process_tags(doc.getroot(), conds, defs, res) 

def process_file(file):
    doc = etree.parse(open(file))
    if doc.getroot().tag != 'Root': return # not process
    outf = os.path.splitext(file)[0] + '.adl'
    conds = []
    defs = {}
    res = etree.Element('Addons')
    
    for cond in doc.xpath('/Root/PropertyCondition'):
        conds.append(cond)

    for defx in doc.xpath('/Root/Define'):
        defs[defx.attrib['name']] = defx.attrib['value']
    
    process_tags(doc.getroot(), conds, defs, res)

    etree.ElementTree(res).write(open('../lib/' + outf, 'w'))    

for fn in os.listdir('.'):
    if fn.lower().endswith('.xml'):
        process_file(fn)

res = etree.Element('Addons')
for fn in os.listdir('common'):
    if fn.lower().endswith('.adx'):
        doc = etree.parse(open('common/'+fn))
        res.append(copy.deepcopy(doc.getroot()))
etree.ElementTree(res).write(open('../lib/common.adl', 'w'))

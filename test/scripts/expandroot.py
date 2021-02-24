def main():
    pass
    ctrl1 = root.Controls['splitContainer2']
    ctrl2 = ctrl1.Panel1
    ctrl3 = ctrl2.Controls['splitContainer1']
    ctrl4 = ctrl3.Panel1
    ctrl5 = ctrl4.Controls['splitContainer3']
    ctrl6 = ctrl5.Panel1
    ctrl7 = ctrl6.Controls['panelTrees']
    ctrl8 = ctrl7.Controls['tabControl1']
    ctrl9 = ctrl8.Controls['tabPage1']
    ctrl10 = ctrl9.Controls['treDataTree']
    ctrl11 = ctrl10.Controls['tree']
    obj1 = FindNodeByPath(ctrl11, [])
    obj1.RealNode.Expand(); DoEvents()

if procedure is None: main()
if procedure == 'main': main()

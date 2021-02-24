def func1():
    pass
    ctrl4 = root.Controls['tbxValue']
    ctrl4.Text = var4;DoEvents()
    ctrl2 = root.Controls['btnOk']
    ctrl2.PerformClick();DoEvents()
    root.Close();DoEvents()

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
    obj1 = FindNodeByPath(ctrl11, [var2])
    obj1.DoubleClick(); DoEvents()
    obj1.RealNode.Expand(); DoEvents()
    obj2 = FindNodeByPath(ctrl11, [var2,var3])
    obj2.RealNode.Expand(); DoEvents()
    obj2.RealNode.Expand(); DoEvents()
    popupMenuObject = obj2
    SetWindowProc('func1')
    obj2.RealNode.Expand(); DoEvents()
    RunPopupMenuCommand(popupMenuObject, 's_create_database'); DoEvents()
    obj2.RealNode.Expand(); DoEvents()

if procedure is None: main()
if procedure == 'main': main()
if procedure == 'func1': func1()

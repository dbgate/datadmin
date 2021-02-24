def func2():
    pass
    ctrl5 = root.Controls['ConnFrame']
    ctrl6 = ctrl5.Controls['layTable']
    ctrl12 = ctrl6.Controls['tbxDataSource']
    ctrl12.Text = var3;DoEvents()
    ctrl4 = root.Controls['btnOk']
    ctrl4.PerformClick();DoEvents()
    root.Close();DoEvents()

def func1():
    pass
    ctrl6 = root.Controls['lbxGroups']
    if ctrl6.FocusedItem is not None: ctrl6.FocusedItem.Selected = False
    ctrl6.FocusedItem = ctrl6.Items.Find('connections', False)[0]; DoEvents()
    ctrl6.FocusedItem.Selected = True; DoEvents()
    ctrl5 = root.Controls['lbxItems']
    if ctrl5.FocusedItem is not None: ctrl5.FocusedItem.Selected = False
    ctrl5.FocusedItem = ctrl5.Items.Find('mssql', False)[0]; DoEvents()
    ctrl5.FocusedItem.Selected = True; DoEvents()
    ctrl2 = root.Controls['tbxNewName']
    ctrl2.Text = var2;DoEvents()
    SetWindowProc('func2')
    ctrl4 = root.Controls['btnOk']
    ctrl4.PerformClick();DoEvents()
    root.Close();DoEvents()

def main():
    pass
    SetWindowProc('func1')
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
    ctrl81 = root.Controls['tstMain']
    menu45 = ctrl81.Items['btnConnect']
    menu45.PerformClick();DoEvents()
    obj1.RealNode.Expand(); DoEvents()

if procedure is None: main()
if procedure == 'main': main()
if procedure == 'func1': func1()
if procedure == 'func2': func2()

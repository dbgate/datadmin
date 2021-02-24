def func2():
    pass
    ctrl5 = root.Controls['ConnFrame']
    ctrl6 = ctrl5.Controls['layTable']
    ctrl8 = ctrl6.Controls['tbxDataSource']
    ctrl8.Text = 'localhost';DoEvents()
    ctrl10 = ctrl6.Controls['tbxLogin']
    ctrl10.Text = 'root';DoEvents()
    ctrl12 = ctrl6.Controls['tbxPassword']
    ctrl12.Text = 'n3j4k3h3sl0';DoEvents()
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
    ctrl5.FocusedItem = ctrl5.Items.Find('mysql', False)[0]; DoEvents()
    ctrl5.FocusedItem.Selected = True; DoEvents()
    ctrl2 = root.Controls['tbxNewName']
    SetWindowProc('func2')
    ctrl2.Text = 'test-mysql';DoEvents()
    ctrl4 = root.Controls['btnOk']
    ctrl4.PerformClick();DoEvents()
    root.Close();DoEvents()

def main():
    pass
    SetWindowProc('func1')
    ctrl77 = root.Controls['tstMain']
    menu45 = ctrl77.Items['btnConnect']
    menu45.PerformClick();DoEvents()

if procedure is None: main()
if procedure == 'main': main()
if procedure == 'func1': func1()
if procedure == 'func2': func2()

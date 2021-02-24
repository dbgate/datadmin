ProgramTitle = 'DatAdmin for EffiProz'
ProgramFolder = 'EffiProzAdmin'
HidePurchaseLinks = '1'
DenyCustomLicenses = '1'
LicenseInfo = """<div class="infobox" style="background-color:#FFFFEE">This user interface is powered by 
<a href='http://datadmin.com'>DatAdmin</a>. 
You can <a href='http://datadmin.com/en/download'>download</a> also versions for other database engines.<br>
Thanks for using EffiProz!
</div>"""
HideLicenseInfo = '0'
LicenseOverride = """<Product name="effiproz" text="EffiProz" longtext="EffiProz Tools">
	<ActiveTo>4000-01-01T00:00:00</ActiveTo>
	<SupportTo>0001-01-01T00:00:00</SupportTo>
	<UpdatesTo>4000-01-01T00:00:00</UpdatesTo>
	
	<Feature name="csv"/>
	<Feature name="dbdocswriter"/>
	<Feature name="excelexport"/>
	<Feature name="tabletextexport"/>
    <Feature name="querydesigner"/>
	<Feature name="diagrams"/>
	<Feature name="depbrowse"/>
	<Feature name="advperspectives"/>
	<Feature name="customdashboards"/>
	<Feature name="extendedfileplaces"/></Product>
""";

RootFilter = 'install'
PluginFilter = 'Plugin.Csv.dll;Plugin.diagrams.dll;Plugin.dbmodel.dll;Plugin.effiproz.dll;Plugin.querytool.dll'
MainWinIcon = 'Plugin.effiproz/Resources/effiproz32.png'
ProgramIcon = 'Plugin.effiproz/Resources/effiproz32.ico'
HideGroupsInCreateDialog = '1'
DatabaseSamplesFolder = '../Sample Databases'
AllowAutoUpdate = '0'

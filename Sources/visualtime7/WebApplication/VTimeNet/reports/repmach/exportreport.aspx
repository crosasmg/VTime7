<%		
    'UPGRADE_WARNING: Date was upgraded to Today and has a new behavior. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1041.aspx'
    Dim mstrExportPath  As String =String.Empty
    mstrExportPath = mstrExportPath & UCase(Replace(Session("wrptName"), ".rpt", "", 1)) & Year(Today) & Month(Today) & Microsoft.VisualBasic.Day(Today) & Hour(TimeOfDay) & Minute(TimeOfDay) & Second(TimeOfDay) & ".TXT"
		session("owRpt").ExportOptions.DestinationType = 1
		session("owRpt").ExportOptions.FormatType = 8
		session("owRpt").ExportOptions.DiskFileName = mstrExportPath
		session("owRpt").Export(False)
		%>





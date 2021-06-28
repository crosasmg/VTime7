<%
    '+Se crea aplicacion CrystalReport
    If IsNothing(session("owApp")) Then
        Session("owApp") = New CRAXDRT.Application
    End If

    If Not IsNothing(session("owRpt")) Then
        'UPGRADE_NOTE: Object session() may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
        session("owRpt") = Nothing
    End If

    '+Se crea objeto reporte desde aplicacion CrystalReport
    session("owRpt") = session("owApp").OpenReport(session("wrptName"), 1)

    session("owRpt").MorePrintEngineErrorMessages = False
    session("owRpt").EnableParameterPrompting = False
%>





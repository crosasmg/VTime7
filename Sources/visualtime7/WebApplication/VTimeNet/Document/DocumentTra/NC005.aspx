<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClaim" %>
<script language="VB" runat="Server">

Dim mobjNetFrameWork As eNetFrameWork.Layout


Dim mSaldo As Double
Dim var_sKey As Object
    Dim mobjValues As eFunctions.Values
        Dim mobjMenues As eFunctions.Menues
    Dim mobjNC005 As eClaim.Document_Pay
    Dim mobjGrid As eFunctions.Grid
 

'+ insDefineHeader: Definición del Grid de consulta de documentos asignados
'-------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	
'UPGRADE_NOTE: The 'eFunctions.Grid' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	mobjGrid = New eFunctions.Grid
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 11.11.57
	mobjGrid.sSessionID = Session.SessionID
	mobjGrid.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjGrid.sCodisplPage = "NC005"
	
	
	With mobjGrid.Columns
		'	Call .AddCheckColumn(0,"Anular","chkDelete","")
		
		Call .AddCheckColumn(0, "Seleccionar", "chkSel", "",  ,  , "insChangeMount(this,this.value)")
		Call .AddNumericColumn(0, "Orden de servicio", "tcnServ_order", 10, 0,  , "Número Orden de Servicio")
		Call .AddNumericColumn(0, "Siniestro", "tcnClaim", 10, 0,  , "Número de Siniestro")
		Call .AddPossiblesColumn(0, "Tipo de documento", "cbeTypesupport", "table5570", 1)
		Call .AddNumericColumn(0, "Numero", "tcnN_Document", 10, 0,  , "Número de Documento")
		Call .AddNumericColumn(0, "Monto", "tcnMount_Document", 10, 0,  , "Número de inicio", 1)
		
	End With
	
	With mobjGrid
		
		.AddButton = False
		.DeleteButton = False
		.Codispl = "NC005"
		.Width = 650
		.Height = 300
		.ActionQuery = mobjValues.ActionQuery
		
		'.Columns("cbeBranch").EditRecord = True
		.nMainAction = Request.QueryString.Item("nMainAction")
		.Columns("Sel").GridVisible = False
		'.WidthDelete = 500
		'.sDelRecordParam = "' + marrArray[lintIndex].sParam + '"
		
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
	
	
End Sub

'%inspreNC002: Se Actualiza el registro seleccionado en el Grid
'-------------------------------------------------------------------------------------------
'--------------------------------------------------------------------------------------------
Private Sub inspreNC005()
	'--------------------------------------------------------------------------------------------
        Dim lcolDocument_Pays As eClaim.Document_Pays
	Dim lclsDocument_Pay As eClaim.Document_Pay
	Dim res As Boolean
	Dim mND As Double
	Dim mNC As Double
	Dim c As Byte
	Dim monto As Double
	
'UPGRADE_NOTE: The 'eDocument.Document_Pays' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	lcolDocument_Pays = New eClaim.Document_Pays
	
	c = 1
	res = lcolDocument_Pays.Find_NC005(Session("sClient"))
	mSaldo = 0
	
	With mobjGrid
		If res Then
			For	Each lclsDocument_Pay In lcolDocument_Pays
				
				If lclsDocument_Pay.nTypesupport = 8 Then
					monto = lclsDocument_Pay.nDebit
					mND = mND + monto
				Else
					monto = lclsDocument_Pay.nCredit
					mNC = mNC + monto
				End If
				
				mSaldo = mNC - mND
				
				.Columns("chkSel").DefValue = lclsDocument_Pay.nTypesupport & "-" & monto & "-" & lclsDocument_Pay.nDocument
				.Columns("tcnServ_order").DefValue = lclsDocument_Pay.nServ_order
				.Columns("tcnClaim").DefValue = lclsDocument_Pay.nClaim
				.Columns("cbeTypesupport").DefValue = lclsDocument_Pay.nTypesupport
				.Columns("tcnN_Document").DefValue = lclsDocument_Pay.nDocument
				.Columns("tcnMount_Document").DefValue = monto
				
				If c = 1 Then
					var_sKey = lclsDocument_Pay.sKey
					c = 0
				End If
				
				monto = 0
				Response.Write(.DoRow)
			Next lclsDocument_Pay
			
		End If
	End With
	
	
	Response.Write(mobjGrid.closeTable())
	
	'UPGRADE_NOTE: Object lcolDocument_Pays may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lcolDocument_Pays = Nothing
	'UPGRADE_NOTE: Object lclsDocument_Pay may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	lclsDocument_Pay = Nothing
	'UPGRADE_NOTE: Object mobjGrid may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
	mobjGrid = Nothing
	
	
    End Sub
    
    
    
    'inspreNC002_upd: Se Actualiza el registro seleccionado en el Grid
    '--------------------------------------------------------------------------------------------
     Sub inspreNC005_upd()
        '--------------------------------------------------------------------------------------------
        Dim lclsDoc_Pay As eClaim.Document_Pay
        lclsDoc_Pay = New eClaim.Document_Pay
        ' Set lclsDoc_Pay = Server.CreateObject("eDocument.Document_Pay")
    
        ' With mobjValues
        '    If Request.QueryString("Action") = "Del" Then
        '       Response.Write mobjValues.ConfirmDelete()
                       
        '      lclsDoc_Pay.insPostNC002 1, _
        '                              .StringToType(Request.QueryString("nTypesupport"), etdLong), _
        '                             Request.QueryString("sClient"), _
        '                            .StringToType(Request.QueryString("nProvider"), etdLong), _
        '                           .StringToType(Request.QueryString("nDocument"), etdLong)
                                        
        ' End If
        Response.Write(mobjGrid.DoFormUpd(Request.QueryString("Action"), "valNC002tra.asp", "NC002", Request.QueryString("nMainAction"), mobjValues.ActionQuery, Request.QueryString("Index")))
        'End With
        ' Set lclsDoc_Pay = Nothing
    End Sub

</script>
<%Response.Expires = -1441


%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>

	<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
	
			
<SCRIPT>
//- Variable para el control de versiones 
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"  

//---------------------------------------------------------------------------------------------
function insChangeMount(Field,value){
//---------------------------------------------------------------------------------------------
  var val;
  var total;
  var cad;
   
  val = value.split("-"); 
  
  cad = self.document.forms[0];
     
  TypDoc = insConvertNumber(val[0]);
  Mount	 = insConvertNumber(val[1]);
  Nid	 = insConvertNumber(val[2]);
  
  Mount_T = insConvertNumber(cad.tcnMount_Total.value);
  
  total = 0;
  	 
		if(Field.checked)
		{
			if(TypDoc == 8)
			{
				total = (Mount_T - Mount);
							
			}else{
					total = (Mount_T + Mount);
				 }
					 
			sw = 1;   				 
		}else{
				if(TypDoc == 8)
				{
					total = (Mount_T + Mount);
								
				}else{
						total = (Mount_T - Mount);
					
					 }
					 
				sw = 0;		 
			 }	
  	
  		insDefValues('Move',"nId="+ Nid + "&nTs="+ TypDoc +"&Sw="+ sw,'/VTimeNet/Document/DocumentTra','showdefNC005');
  		
  		//(sValue, sCurDecimalPoint, sNewDecimalPoint, sThousandsChar, nDecimals, bFormatJS)
  		
  		
  		cad.tcnMount_Total.value = VTFormat(total,0,0,1,0,true);//insConvertNumber(total);
  		  			
  		  		  
}

</SCRIPT>

	<%
'UPGRADE_NOTE: The 'eFunctions.Values' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
mobjValues =  New eFunctions.Values


Response.Write(mobjValues.ShowWindowsName("NC005"))

mobjValues.sCodisplPage = "NC005"

Response.Write(mobjValues.StyleSheet())

'UPGRADE_NOTE: The 'eFunctions.Menues' object is not registered in the migration machine. Copy this link in your browser for more: ms-its:C:\Archivos de programa\ASP to ASP.NET Migration Assistant\AspToAspNet.chm::/1016.htm
	    mobjMenues = New eFunctions.Menues
	    
Response.Write("<SCRIPT>var	nMainAction	= 0" & Request.QueryString.Item("nMainAction") & "</SCRIPT>")
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenues.setZone(2, "NC005", "NC005.aspx"))
End If
If CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401 Then
	mobjValues.ActionQuery = True
End If

'UPGRADE_NOTE: Object mobjMenues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjMenues = Nothing
%>

</HEAD>

<BODY ONUNLOAD="closeWindows();">
	<FORM METHOD="POST" ID="FORM" NAME="NC005" ACTION="valNC005Tra.aspx?sZone=2&nMainAction=<%=Request.QueryString.Item("nMainAction")%>">
		<%
Call insDefineHeader()
If Request.QueryString.Item("Type") <> "PopUp" Then
	inspreNC005()
Else
	inspreNC005_upd()
End If
%>
		<TABLE align="right">
		  <TR>
			<TD><LABEL ID=0>Saldo  </LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnMount_Saldo", 10, mSaldo,  , "Saldo Total", 1,  ,  ,  ,  ,  , True)%></TD> 
			<TD><LABEL ID=0>Total  </LABEL></TD>
			<TD><%=mobjValues.NumericControl("tcnMount_Total", 10, 0,  , "Monto Total", 1,  ,  ,  ,  ,  , True)%></TD> 
		  </TR>
			<%=mobjValues.HiddenControl("sKey", var_sKey)%>
			<%=mobjValues.HiddenControl("optTypProcess", 1)%>
		  </TABLE>
		<br>
		<br>
		<%
Response.Write(mobjValues.BeginPageButton)
'UPGRADE_NOTE: Object mobjValues may not be destroyed until it is garbage collected. Copy this link in your browser for more: 'http://msdn.microsoft.com/library/en-us/vbcon/html/vbup1029.aspx'
mobjValues = Nothing



%>
		
	</FORM>
</BODY>
</HTML>







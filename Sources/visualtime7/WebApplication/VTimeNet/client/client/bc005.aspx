<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjClient As eClient.Client
Dim mobjMenu As eFunctions.Menues
Dim mobjGrid As eFunctions.Grid

'- Se define la Variable mblnQuery, como temporal para guardar el valor de
'- la propiedad Action Query del objeto mobjValues.

Dim mblnQuery As Object
Dim mblnClient As Boolean

    

'% insDefineHeader: se definen las propiedades del grid 
'----------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'----------------------------------------------------------------------------------------------
	Dim lobjColumn As Object
	Dim lstrClient As Object
	
	If IsNothing(Session("mobjCollecDel")) Then
            Session("mobjCollecDel") = New Collection
	End If
	
	With mobjGrid.Columns
		Call .AddClientColumn(9486, GetLocalResourceObject("tctNewCodeColumnCaption"), "tctNewCode", "", True, GetLocalResourceObject("tctNewCodeColumnToolTip"), "InsChangeClient()",,,,,,,,,,,,,,,True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeSexclienColumnCaption"), "cbeSexclien", "table18", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeSexclienColumnToolTip"))
		Call .AddDateColumn(0, GetLocalResourceObject("tcdBirthdateColumnCaption"), "tcdBirthdate", "",  , GetLocalResourceObject("tcdBirthdateColumnToolTip"),  ,  ,  , True)
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeOccupatColumnCaption"), "cbeOccupat", "table16", eFunctions.Values.eValuesType.clngWindowType,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeOccupatColumnToolTip"))
		Call .AddCheckColumn(0, GetLocalResourceObject("chkSmokingColumnCaption"), "chkSmoking", "",  ,  ,  , True, GetLocalResourceObject("chkSmokingColumnToolTip"))
		Call .AddAnimatedColumn(100693, GetLocalResourceObject("btnQueryColumnCaption"), "btnQuery", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnQueryColumnToolTip"))
		
		If CStr(Session("OptAction")) = "1" Then
			Call .AddAnimatedColumn(0, GetLocalResourceObject("cmdAddressColumnCaption"), "cmdAddress", "/VTimeNet/images/ShowAddress.png", GetLocalResourceObject("cmdAddressColumnToolTip"))
		End If
		
		.AddHiddenColumn("hddClient", "")
		.AddHiddenColumn("hddsClient", "")
	End With
	
	With mobjGrid
		.Codispl = "BC005"
		.Left = 150
		.Width = 500
		.Height = 300
		
		.Columns("btnQuery").HRefScript = "ShowPopUp('/VTimeNet/Common/SCA006.aspx?sCodispl=SCA006&nMainAction=' + nMainAction + '&sClient=' + mstrClient,'BC005',450,300,'no','no',200,80)"
		
		'+ Si la opcion es cambio de codigo la ventana no debe recargarse.        
		If Request.QueryString.Item("Type") = "PopUp" And CStr(Session("OptAction")) = "1" Then
			mobjGrid.bCheckVisible = False
		End If
		If CStr(Session("OptAction")) = "1" Then
			.AddButton = CBool(Session("ButtomAdd"))
		Else
			.AddButton = True
		End If
		
		If CBool(Session("ButtomAdd")) Then
			.sReloadAction = Request.QueryString.Item("ReloadAction")
			If Request.QueryString.Item("Reload") = "1" Then
				.sReloadIndex = Request.QueryString.Item("ReloadIndex")
			End If
		End If
		.nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") ="", 0, Request.QueryString.Item("nMainAction")))
		.sDelRecordParam = "sDelClient='+ marrArray[lintIndex].tctNewCode + '"
		.sReloadAction = Request.QueryString.Item("ReloadAction")
		If Request.QueryString.Item("Reload") = "1" Then
			.sReloadIndex = Request.QueryString.Item("ReloadIndex")
		End If
		
	End With
	
End Sub
'----------------------------------------------------------------------------------------------
Private Sub insPreBC005()
	'----------------------------------------------------------------------------------------------
	mobjClient = New eClient.Client
	mblnClient = mobjClient.Find(Session("sCodeClient"), True)
        If CStr(Session("OptAction")) = "2" Then
            

            Response.Write("" & vbCrLf)
            Response.Write("	<TABLE WIDTH=100% COLSPAN=""4"" BORDER=0>        	    " & vbCrLf)
            Response.Write("	    ")

            If mobjClient.nPerson_typ = 2 Then
                Response.Write("" & vbCrLf)
                Response.Write("	           <TR><TD><LABEL>" & GetLocalResourceObject("cbeOccupatCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("			       <TD>")


                Response.Write(mobjValues.PossiblesValues("cbeOccupat", "Table417", eFunctions.Values.eValuesType.clngComboType, CStr(mobjClient.nSpeciality), , , , , , , True, , GetLocalResourceObject("cbeOccupatToolTip")))


                Response.Write("</TD> 	    	    " & vbCrLf)
                Response.Write("               </TR>  " & vbCrLf)
                Response.Write("        ")

            Else
                Response.Write("" & vbCrLf)
                Response.Write("	           <TR>" & vbCrLf)
                Response.Write("	               <TD><LABEL>" & GetLocalResourceObject("cbeSexclienCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("			       <TD>")


                Response.Write(mobjValues.PossiblesValues("cbeSexclien", "Table18", eFunctions.Values.eValuesType.clngComboType, mobjClient.sSexclien, , , , , , , True, , GetLocalResourceObject("cbeSexclienToolTip")))


                Response.Write("</TD> 			  " & vbCrLf)
                Response.Write("			       <TD><LABEL>" & GetLocalResourceObject("tcdBirthDateCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("			       <TD>")


                Response.Write(mobjValues.DateControl("tcdBirthDate", CStr(mobjClient.dBirthdat), , GetLocalResourceObject("tcdBirthDateToolTip"), , , , , True))


                Response.Write(" </TD>" & vbCrLf)
                Response.Write("	           </TR>                            " & vbCrLf)
                Response.Write("	           <TR>" & vbCrLf)
                Response.Write("			       <TD><LABEL>" & GetLocalResourceObject("cbeOccupatCaption") & "</LABEL></TD>" & vbCrLf)
                Response.Write("			       <TD>")


                Response.Write(mobjValues.PossiblesValues("cbeOccupat", "Table16", eFunctions.Values.eValuesType.clngComboType, CStr(mobjClient.nSpeciality), , , , , , , True, , GetLocalResourceObject("cbeOccupatToolTip")))


                Response.Write("</TD>" & vbCrLf)
                Response.Write("			       ")

                If mobjClient.sSmoking = "1" Then
                    Response.Write("" & vbCrLf)
                    Response.Write("	    	              <TD>")


                    Response.Write(mobjValues.CheckControl("chkSmoking", GetLocalResourceObject("chkSmokingCaption"), "1", , , True))


                    Response.Write("</TD>" & vbCrLf)
                    Response.Write("                   ")

                Else
                    Response.Write("" & vbCrLf)
                    Response.Write("	    	              <TD>")


                    Response.Write(mobjValues.CheckControl("chkSmoking", GetLocalResourceObject("chkSmokingCaption"), CStr(False), , , True))


                    Response.Write("</TD>            " & vbCrLf)
                    Response.Write("                   ")

                End If
                Response.Write("" & vbCrLf)
                Response.Write("        ")

            End If
            Response.Write("                " & vbCrLf)
            Response.Write("        ")

            Response.Write("<SCRIPT> var mstrClient = '" & Request.QueryString.Item("sClient") & "';</" & "Script>")
            Response.Write("                       " & vbCrLf)
            Response.Write("	          </TR>" & vbCrLf)
            Response.Write("	    ")

            If mobjClient.nPerson_typ = 1 Then
                Response.Write("	    " & vbCrLf)
                Response.Write("	           <TR>" & vbCrLf)
                Response.Write("			       <TD><LABEL>" & GetLocalResourceObject("btnPolicyValuesCaption") & "</LABEL></TD>            " & vbCrLf)
                Response.Write("                   <TD>")


                Response.Write(mobjValues.AnimatedButtonControl("btnPolicyValues", "/VTimeNet/images/btn_ValuesOff.png", GetLocalResourceObject("btnPolicyValuesToolTip"), , "ShowSports()", mobjClient.nPerson_typ = 2))


                Response.Write("</TD>	    " & vbCrLf)
                Response.Write("	           </TR>" & vbCrLf)
                Response.Write("	    ")

            End If
            Response.Write("       " & vbCrLf)
            Response.Write("	    <TR>	    " & vbCrLf)
            Response.Write("	        <TD>")


            Response.Write(mobjValues.HiddenControl("hddsClient", Session("sCodeClient")))


            Response.Write("</TD>	                	    " & vbCrLf)
            Response.Write("	    	<TD>&nbsp;</TD>" & vbCrLf)
            Response.Write("	    </TR>	    		        " & vbCrLf)
            Response.Write("	</TABLE>    ")

        End If
	
        Dim lstrClient As String
        Dim lstrClientDel As String = String.Empty
        Dim lstrQString As String
        Dim lclsClient As eClient.Client
        Dim lblnChecked As Boolean
        Dim lblnPerson_typ As Boolean
	
        Response.Write(mobjValues.ShowWindowsName("BC005"))
	
        lclsClient = New eClient.Client
	
        lstrQString = vbNullString
	
        If Not IsNothing(Request.QueryString.Item("sAuxClient")) Then
            For Each lstrClient In Request.QueryString.Item("sAuxClient").ToString.Split(",")
                With mobjGrid
                    If Session("mobjCollecDel").Count > 0 Then
                        If Session("mobjCollecDel").Exists(lstrClient) Then
                            lstrClientDel = lstrClient
                        Else
                            lstrClientDel = ""
                        End If
                    End If
                    
                    If lstrClient <> vbNullString And lstrClient <> lstrClientDel Then
                        lstrQString = lstrQString & "sAuxClient" & "=" & lstrClient & "&"
                    End If
                    
                End With
            Next lstrClient
        End If
        
        If Not IsNothing(Request.QueryString.Item("sAuxClient")) Then
            For Each lstrClient In Request.QueryString.Item("sAuxClient").ToString.Split(",")
                With mobjGrid
                    lstrClient = lclsClient.ExpandCode(lstrClient)
                    If Session("mobjCollecDel").Count > 0 Then
                        If Session("mobjCollecDel").Exists(lstrClient) Then
                            lstrClientDel = lstrClient
                        Else
                            lstrClientDel = String.Empty
                        End If
                    End If
                    
                    If Not String.IsNullOrEmpty(lstrClient) And lstrClient <> lstrClientDel Then
                        If lclsClient.Find(lstrClient) Then
                            If lclsClient.sSmoking = "1" Then
                                lblnChecked = True
                            Else
                                lblnChecked = False
                            End If
						
                            If lclsClient.nPerson_typ = 1 Then
                                lblnPerson_typ = False
                            Else
                                lblnPerson_typ = True
                            End If
						
                            mobjGrid.Columns("cbeSexclien").DefValue = lclsClient.sSexclien
                            mobjGrid.Columns("tcdBirthdate").DefValue = mobjValues.TypeToString(lclsClient.dBirthdat, eFunctions.Values.eTypeData.etdDate)
						
                            If lclsClient.nPerson_typ = 1 Then
                                mobjGrid.Columns("cbeOccupat").TableName = "Table16"
                            Else
                                mobjGrid.Columns("cbeOccupat").TableName = "Table417"
                            End If
						
                            mobjGrid.Columns("cbeOccupat").DefValue = mobjValues.TypeToString(lclsClient.nSpeciality, eFunctions.Values.eTypeData.etdDouble)
                            mobjGrid.Columns("chkSmoking").Checked = mobjValues.StringToType(lclsClient.sSmoking, eFunctions.Values.eTypeData.etdDouble)

					
                            .sDelRecordParam = .sDelRecordParam & "&" & lstrQString
                            .sEditRecordParam = lstrQString
                            .Columns("tctNewCode").DefValue = lstrClient
					
                            .Columns("btnQuery").Disabled = lblnPerson_typ
                            .Columns("hddClient").DefValue = lstrClient
					
                            If CStr(Session("OptAction")) = "1" Then
                                .Columns("cmdAddress").HRefScript = "ShowPopUp('/VTimeNet/Common/SCA001.aspx?sCodispl=SCA101&sOnSeq=2&nMainAction=401&sClient=" & lstrClient & "','ShowAddress',500,500,'yes','yes','no','no')"
                            End If
					
                            Response.Write("<SCRIPT> var mstrClient = '" & lstrClient & "';</" & "Script>")
					
                            Response.Write(.DoRow)
                        Else
                            mobjGrid.AddButton = True
                        End If
                    
                    End If
                End With
            Next lstrClient
        End If
	
        If String.IsNullOrEmpty(lstrQString) Then
            Session("ButtomAdd") = True
        Else
            Session("ButtomAdd") = False
        End If
        Session("sVarQString") = lstrQString
        Response.Write(mobjGrid.closeTable())
        Response.Write(mobjValues.BeginPageButton)
	
        Response.Write("" & vbCrLf)
        Response.Write("<SCRIPT>function insShowHeader(){" & vbCrLf)
        Response.Write("    //setTimeout(""insShowHeader()"",50);" & vbCrLf)
        Response.Write("}" & vbCrLf)
        Response.Write("insShowHeader();" & vbCrLf)
        Response.Write("</" & "SCRIPT>")

	
End Sub
'----------------------------------------------------------------------------------------------
Private Sub insPreBC005Upd()
	'----------------------------------------------------------------------------------------------
        Dim lstrClient As String
	
        With Request
            If .QueryString.Item("Action") = "Del" Then
                lstrClient = Request.QueryString.Item("sDelCLient")
                
                If Session("mobjCollecDel").Count > 0 Then
                    If Not Session("mobjCollecDel").Exists(lstrClient) Then
                        Session("mobjCollecDel").add(lstrClient, lstrClient)
                    End If
                End If
                
                If Session("OptAction") = 1 Then
                    Session("ButtomAdd") = True
                End If
            Else
                If Session("mobjCollecDel").Count > 0 Then
                    Session("mobjCollecDel").RemoveAll()
                End If
            End If
            
            Response.Write(mobjGrid.DoFormUpd(Request.QueryString.Item("Action"), "valClient.aspx", Request.QueryString.Item("sCodispl"), Request.QueryString.Item("nMainAction"), False, CShort(Request.QueryString.Item("Index"))))
        
        End With
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjGrid = New eFunctions.Grid
mobjClient = New eClient.Client

If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.setZone(2, "BC005", "BC005.aspx"))
End If
%>
	<SCRIPT LANGUAGE="JavaScript">
	var nMainAction = 304;
    			
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15.57 $" 
   			
	</SCRIPT>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>

	
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
    <%=mobjValues.StyleSheet()%>
<SCRIPT>

//% valDigit: Se verifica que no se introduzca dígito verificador provisional "E".
//-------------------------------------------------------------------------------------------
function valDigit(Field){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]) {
		if (elements[1].value=='E' || elements[1].value=='e') {
			elements[0].value='';
			elements[1].value='';			
		}
	}
}

var mstrClient

//% ShowSports: Muestra los deportes más frecuentes del cliente
//---------------------------------------------------------------------------------------------------------------------------------------------------
function ShowSports(){
//---------------------------------------------------------------------------------------------------------------------------------------------------
	    ShowPopUp('/VTimeNet/Common/SCA006.aspx?sCodispl=SCA006&nMainAction=' + nMainAction + '&sClient=' + mstrClient,'BC005',450,300,'no','no',200,80);
}

// % InsChangeClient: Despliega los datos del cliente
//-------------------------------------------------------------------------------------------
function InsChangeClient(){
//-------------------------------------------------------------------------------------------     
    var lintIndex;
    var error;
    
    insDefValues('Client', "sCodispl=" + 'BC005' + '&sClient=' + self.document.forms[0].tctNewCode.value, '/VTimeNet/Client/Client');

    with (self.document.forms[0])
        mstrClient = self.document.forms[0].tctNewCode.value;                
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows()">
<FORM METHOD="POST" ACTION="valclient.aspx?TIME=1">
<%
mobjClient = New eClient.Client
Call insDefineHeader()
If Request.QueryString.Item("Type") = "PopUp" Then
	Call insPreBC005Upd()
Else
	Call insPreBC005()
End If
%>
</FORM>
</BODY>
</HTML>






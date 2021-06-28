<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eInterface" %>
<%@ Import namespace="eBatch" %>
<script language="VB" runat="Server">

'- Object for the handling of the general functions of load of values
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Object for the handling of the areas of the page
'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues

Dim mobjGrid As Object
Dim mBlnControl As Boolean
Dim mobjInterface As eInterface.MasterSheet
Dim mInterface As Object



'% insDefineHeader: Se carga el combo con los valores de los archivos.
'------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'------------------------------------------------------------------------------
	Dim lstrList_Files As String
    Dim lArrList_Files() As String
	Dim i As Integer
	Dim lclsMasiveCharge As eBatch.MasiveCharge
	
	lclsMasiveCharge = New eBatch.MasiveCharge
	
	lstrList_Files = lclsMasiveCharge.Find_Files("2")
	lArrList_Files = lstrList_Files.Split("|")
	
	For i = 0 To UBound(lArrList_Files)
		Response.Write("<SCRIPT>self.document.forms[0].cbeFile.options[" & i + 1 & "] = new Option('" & lArrList_Files(i) & "','" & lArrList_Files(i) & "');</" & "Script>")
	Next 
	If mInterface <= 0 Or (mInterface > 0 And mobjInterface.nIntertype = 2) Then
            Response.Write("<SCRIPT>")
            Response.Write("ShowDiv('divFile', 'hide');")
            Response.Write("ShowDiv('divFile1', 'hide');")
            Response.Write("ShowDiv('divFile3', 'hide');")
            Response.Write("ShowDiv('divFile4', 'hide');")
            Response.Write("insHandImage('A401', false);")
            Response.Write("</" & "SCRIPT>")
          
            'Response.Write("<SCRIPT>ShowDiv('divFile', 'hide');ShowDiv('divFile1', 'hide');ShowDiv('divFile3', 'hide');ShowDiv('divFile4', 'hide')</" & "Script>")
        End If
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
mobjInterface = New eInterface.MasterSheet

If Request.QueryString.Item("sCodispl") = "GI1402_K" Then
	mInterface = -32768
	mBlnControl = False
	mobjInterface.nIntertype = 1
Else
	mInterface = Mid(Request.QueryString.Item("sCodispl"), 4)
	Call mobjInterface.Find(mInterface)
	mobjInterface.nSheet = mInterface
	mBlnControl = True
End If


%>
<html>
<head>
	<meta NAME="GENERATOR" CONTENT="eTransaction Designer for Visual TIME">
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></script>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></script>



<script>

//% For the Source Safe control
//% Para control de versiones
//------------------------------------------------------------------------------------------
document.VssVersion="$$Revision: 3 $|$$Date: 19/04/06 18:10 $"
//------------------------------------------------------------------------------------------

//% insStateZone: updates the status of the fields in the page
//% insStateZone: habilita los campos de la forma
//------------------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------------------
<%If Not mBlnControl Then%>
 try {  
 with (self.document.forms[0]){
		cbeSystem.disabled=false;
        optnintertype[0].disabled=false;
        optnintertype[1].disabled=(top.frames['fraSequence'].plngMainAction==401);
        if (top.frames['fraSequence'].plngMainAction==401){
            optnintertype[0].checked=true;
            optnintertype[1].checked=false;
            valnsheet.value='';
			UpdateDiv('valnsheetDesc','','Normal');				
			valnsheet.Parameters.Param1.sValue='1';
        }
        cbeFile.disabled=(top.frames['fraSequence'].plngMainAction==401);
        if (top.frames['fraSequence'].plngMainAction!=401 && optnintertype[1].checked==false){
            ShowDiv('divFile', 'show');
            ShowDiv('divFile1', 'show');        
        }
        else{
            ShowDiv('divFile', 'hide');
            ShowDiv('divFile1', 'hide');
        }
        tctFile.disabled=(top.frames['fraSequence'].plngMainAction==401);
        if (top.frames['fraSequence'].plngMainAction!=401 && optnintertype[1].checked==false){
            ShowDiv('divFile3', 'show');
            ShowDiv('divFile4', 'show');        
        }
        else {
            ShowDiv('divFile3', 'hide');
            ShowDiv('divFile4', 'hide'); 
        }
        if (optnintertype[1].checked==true)
            insHandImage('A401', false);
    }}

 catch(x) {}
<%Else%>
try {  
 with (self.document.forms[0]){
		if (top.frames['fraSequence'].plngMainAction==401){
            ShowDiv('divFile', 'hide');
            ShowDiv('divFile1', 'hide');
             ShowDiv('divFile3', 'hide');
            ShowDiv('divFile4', 'hide');        
        }
        else
        {
            if(optnintertype[0].checked==true){
                ShowDiv('divFile', 'show');
                ShowDiv('divFile1', 'show');
                ShowDiv('divFile3', 'show');
                ShowDiv('divFile4', 'show');
                tctFile.disabled=false;
            }
            else
                insHandImage('A401', false);
        }
    }}

 catch(x) {}
<%End If%>
  
}

//% insCancel: It executes necessary routines at the moment for cancelling the page
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    insDefValues("UpdStatus")
}

//% InsChangeField: Carga sheet(planillas) por tipo de interfaz y tabla de sistema externo.
//------------------------------------------------------------------------------------------
function InsChangeField(vObj, sField, sValCheck){
	var sValue;
	
	sValue = vObj.value;
//limpio campos 
	UpdateDiv('tcnopertype','','Normal');
	UpdateDiv('tcnformat','','Normal');
	
	with (self.document.forms[0]){
		switch (sField){
			case 'optnintertype':

				if (cbeSystem.value=''){
				    valnsheet.disabled = true;
				    btnvalnsheet.disabled = true;
				}
				else{
				    valnsheet.disabled = true;
				    btnvalnsheet.disabled = true;
				}

				if (sValue=="2"){
				    ShowDiv('divFile', 'hide');
				    ShowDiv('divFile1', 'hide');
				    ShowDiv('divFile3', 'hide');
				    ShowDiv('divFile4', 'hide');
                    insHandImage('A401', false);
                    top.frames['fraSequence'].plngMainAction=304;
				}
				else{
                    if (top.frames['fraSequence'].plngMainAction!=401){
				        ShowDiv('divFile', 'show');
				        ShowDiv('divFile1', 'show');				    
				        ShowDiv('divFile3', 'show');
				        ShowDiv('divFile4', 'show');	
                    }			    
                    insHandImage('A401', true);
				}
				    
				valnsheet.value='';
				UpdateDiv('valnsheetDesc','','Normal');				
				valnsheet.Parameters.Param1.sValue=sValCheck;
 				break;
			case 'cbeSystem':
				valnsheet.Parameters.Param2.sValue=sValue;
				tctFile.disabled = true;
				if (sValue != ''){
					valnsheet.disabled = false;
					btnvalnsheet.disabled = false;
					valnsheet.value='';
					UpdateDiv('valnsheetDesc','','Normal');
				}
 				break;
 			case 'cbeFile':
 			    if (sValue != ''){
			        hdtFileName.value = cbeFile.value;
			        tctFile.disabled = true;
			    } else {
			        tctFile.disabled = false;
			    }
 				break;
            case 'tctFile':
 			    if (sValue != ''){
			        cbeFile.disabled = true;
			        cbeFile.value = '';
			    } else {
			        cbeFile.disabled = false;
			    }
 				break; 				

			case 'valnsheet':
			    if(valnsheet_nOpertype.value == '-32768'){
			         UpdateDiv('tcnopertype','','Normal');
			    }
			    else{
			         UpdateDiv('tcnopertype',valnsheet_nOpertype.value + ' ' + valnsheet_sOpertype.value,'Normal');
			    }
				UpdateDiv('tcnformat',valnsheet_nFormat.value + ' ' + valnsheet_sFormat.value,'Normal');
				<%If Not mBlnControl Then%>
				if(valnsheet_nFormat.value == '3'){
					tctTable.disabled = false;
					tctFile.value = '';
                    tctFile.disabled = true;
					cbeFile.value = '';
					cbeFile.disabled = true;

    			}
				if(valnsheet_nFormat.value == '1' || valnsheet_nFormat.value == '2' || valnsheet_nFormat.value == '4' || valnsheet_nFormat.value == '11')
				{	
				    if(cbeFile.value != ''){
				        tctFile.disabled = true;
                        tctTable.disabled= true;
				    } else {
				        tctFile.disabled = false;
				        tctTable.disabled= false;
                    }

                    
				}
				<%End If%>
 				break; 				
 		}
 	}
}		
//%InsClickValues: Carga valor del campo tctFileName
//--------------------------------------------------------------------------------    
function InsClickValues(Obj){
//--------------------------------------------------------------------------------    
	var i
	with(self.document.forms[0]){
		for (i in marrArray){
			if (i!=Obj.value)
				Sel[i].checked = false;
		}

		if (Obj.checked)
			hdtFileName.value = marrArray[Obj.value].tctFile;
		else
			hdtFileName.value = "";
	}
}
</script>
<%
Response.Write(mobjValues.StyleSheet())
mobjMenu = New eFunctions.Menues

Response.Write(mobjMenu.MakeMenu("GI1402_K", "GI1402_k.aspx", 1, vbNullString))

Response.Write("<SCRIPT>var nMainAction = top.frames['fraSequence'].plngMainAction</SCRIPT>")


'+ Indicador para impresion del Reporte Errores
session("Report") = "N"

    mobjMenu = Nothing
%>    
</HEAD>
	<BODY ONUNLOAD="closeWindows();">
		<FORM METHOD="POST" ID="FORM" NAME="GI1402_K" ACTION="valinterfaceseq.aspx?smode=2" ENCTYPE="multipart/form-data">
			<BR>
			<BR>
<%
If Request.QueryString.Item("sCodispl") <> "GI1402_K" Then
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))
End If
%>
			<TABLE WIDTH="100%">
				<TR>
					<TD WIDTH="70%">
						<TABLE WIDTH="100%">
							<TR>
								<TD><LABEL><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
								 <%If mobjInterface.nIntertype = 1 Then%>
									<TD><%=mobjValues.OptionControl(0, "optnintertype", GetLocalResourceObject("optnintertype_1Caption"), CStr(1), "1", "InsChangeField(this, ""optnintertype"",1)", True)%></TD>
									<TD><%=mobjValues.OptionControl(0, "optnintertype", GetLocalResourceObject("optnintertype_2Caption"),  , "2", "InsChangeField(this, ""optnintertype"",2)", True)%></TD>
								 <%Else%>
									<TD><%=mobjValues.OptionControl(0, "optnintertype", GetLocalResourceObject("optnintertype_1Caption"),  , "1", "InsChangeField(this, ""optnintertype"",1)", True)%></TD>
									<TD><%=mobjValues.OptionControl(0, "optnintertype", GetLocalResourceObject("optnintertype_2Caption"), CStr(1), "2", "InsChangeField(this, ""optnintertype"",2)", True)%></TD>
								 <%End If%>
 							</TR>
							<TR>
								<TD>
									<LABEL><%= GetLocalResourceObject("cbeSystemCaption") %></LABEL>
								</TD>
								<TD>
									<%=mobjValues.PossiblesValues("cbeSystem", "Table5705", eFunctions.Values.eValuesType.clngComboType, CStr(mobjInterface.nSystem),  ,  ,  ,  ,  , "InsChangeField(this,""cbeSystem"")", True,  , GetLocalResourceObject("cbeSystemToolTip"),  , 14)%>
								</TD>					
								<TD>
									<DIV ID="divFile"><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></DIV>
								</TD>
								<TD>
									<DIV ID="divFile1">
									<%If (mInterface > 0 And mobjInterface.nIntertype = 1) Then%>
										<SELECT SIZE="1" NAME="cbeFile" TABINDEX=15 TITLE="Archivos a cargar" ONFOCUS="ChangeFocus(this)" ONCHANGE='InsChangeField(this,"cbeFile")'></SELECT>
									<%Else%>
									    <SELECT SIZE="1" NAME="cbeFile" TABINDEX=15 TITLE="Archivos a cargar" NOTAB DISABLED ONFOCUS="ChangeFocus(this)" ONCHANGE='InsChangeField(this,"cbeFile")'></SELECT>
									<%End If%>
							                <%
							                Response.Write(mobjValues.HiddenControl("hdtFileName", ""))
							                Response.Write(mobjValues.HiddenControl("hddnFormat", CStr(mobjInterface.nFormat)))
							                %>				
							       </DIV>     
								</TD>
							</TR>
							<TR>
								<TD><LABEL><%= GetLocalResourceObject("valnsheetCaption") %></LABEL></TD>
								
                                <!-- 19/11/2019 - José Alvear:                                                                                                    -->
                                <!-- Se actualiza parametros de entrada a tabtable TABTABLEMASTERSHEET ya que envíaba 1 (interfaz de entrada) y ahora es dinamico -->
                                <TD><%
                                        mobjValues.Parameters.Add("NINTERTYPE", mobjInterface.nIntertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                                        mobjValues.Parameters.add("NSYSTEM", mobjInterface.nSystem, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                                        mobjValues.Parameters.ReturnValue("nOpertype",  ,  , True)
                                        mobjValues.Parameters.ReturnValue("sOpertype",  ,  , True)
                                        mobjValues.Parameters.ReturnValue("nFormat",  ,  , True)
                                        mobjValues.Parameters.ReturnValue("sFormat",  ,  , True)
                                        Response.Write(mobjValues.PossiblesValues("valnsheet", "TABTABLEMASTERSHEET", eFunctions.Values.eValuesType.clngWindowType, mInterface, True,  ,  ,  ,  , "InsChangeField(this,""valnsheet"")", True,  , GetLocalResourceObject("valnsheetToolTip"),  , 14))
%>
								</TD>
								<TD>
									<DIV ID="divFile3"><LABEL><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></DIV>
								</TD>
								<TD>
									<DIV ID="divFile4">
					            <%=mobjValues.FileControl("tctFile", 45, , False, , "InsChangeField(this,""tctFile"")")%></TD>
                                </DIV>     
                                </TD>
							   <%Call insDefineHeader()%></TD>

							</TR>				
							<TR>
								<TD><LABEL><%= GetLocalResourceObject("Anchor3Caption") %></LABEL><%=mobjValues.DIVControl("tcnopertype", True, mobjInterface.sOpertype)%></TD>
							</TR>
							<TR>
								<TD><LABEL><%= GetLocalResourceObject("Anchor4Caption") %></LABEL><%=mobjValues.DIVControl("tcnformat", True, mobjInterface.sFormat)%></TD>
			                    <TD COLSPAN="1"><LABEL><%= GetLocalResourceObject("tctTableCaption") %></LABEL></TD>
			                    <TD COLSPAN="1"><%=mobjValues.TextControl("tctTable", 15, , True, , , , , , True)%></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</FORM>
	</BODY>
</HTML>

<%
    If mobjInterface.nIntertype = 2 Then
        Response.Write("<SCRIPT>")
        Response.Write("insHandImage('A401', false);")
        Response.Write("</" & "SCRIPT>")
    Else
        Response.Write("<SCRIPT>")
        Response.Write("insHandImage('A401', true);")
        Response.Write("</" & "SCRIPT>")
    End If
    
mobjInterface = Nothing

mobjValues = Nothing
%>





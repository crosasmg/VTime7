<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'^Begin Header Block VisualTimer Utility 1.1 31/3/03 17.17.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Contador del número de registros insertados en la página
Dim mlngOptionalBeginProcess As Object

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values

'-Objeto para el manejo y evaluación de las instrucciones SQL para las búsquedas.
Dim mcolQueryClients As eClient.QueryClients

'- Primer y último nombre mostrado en cada página.
Dim mintFirstRecord As Object
Dim mintLastRecord As Object

'- Indica el movimiento a efectuar para la búsqueda de los datos. (Next o Previous)    
Dim mstrWay As String

'- Nombre del control destino del código de la selección.
Dim mstrControlName As String

'- Nombre del control destino del nombre de la selección.    

Dim mstrControlClieName As String

'- Cantidad máxima de elementos por página.

Const CN_MAXRECORDS As Short = 20

'- Número de página que se está mostrando
Dim mintPageNumber As Object

'- Variable para el manejo de las funciones del grid.
Dim mobjGrid As eFunctions.Grid

'- Habilita o desabilita las acciones sobre los botones Back y Next.
Dim mblnDisabledBack As Boolean
Dim mblnDisabledNext As Boolean

Dim lobjErrors As eGeneral.GeneralFunction
Dim mstrAlert As String

Dim lintDefvalue As Object


'% ApplyCondition : Ejecuta la consulta según los parámetros dados.
'--------------------------------------------------------------------------------------------
Private Sub ApplyCondition()
	'--------------------------------------------------------------------------------------------
	
	'+ Se inicializan las variables si estas no poseen valor.
	If mintFirstRecord = vbNullString Then
		mintFirstRecord = 1
	End If
	If mintLastRecord = vbNullString Then
		mintLastRecord = mintFirstRecord + CN_MAXRECORDS - 1
	End If
	
	'+ Se inicializa el número de página mostrado.       
	mintPageNumber = 1
	
	'+ Según el tipo de movimiento realizado se construye la cláusula WHERE especial.
	If Request.QueryString.Item("mstrWay") = "Next" Then
		mintFirstRecord = CDbl(Request.Form.Item("hddLastRecord")) + 1
		mintLastRecord = mintFirstRecord + CN_MAXRECORDS - 1
	ElseIf Request.QueryString.Item("mstrWay") = "Back" Then 
		mintFirstRecord = CDbl(Request.Form.Item("hddFirstRecord")) - CN_MAXRECORDS
		mintLastRecord = CDbl(Request.Form.Item("hddFirstRecord")) - 1
	End If
	
	'+ Se realiza la búsqueda de los datos.
        If mcolQueryClients.FindCondition(Request.Form.Item("tctClientCode"), Request.Form.Item("tctClientName"), Request.Form.Item("tctLastName"), Request.Form.Item("tctLastName2"), Request.Form.Item("tcdBirthDate"), Request.Form.Item("cbeSex"), CInt(Request.Form.Item("cbePerson_typ")), , , CShort(mintFirstRecord), CShort(mintLastRecord), 1, Session("STYPEUSER"), Session("sUserClient")) Then
            If mcolQueryClients.Count > 0 Then
                '+ Se obtiene el número del primer elemento de la página.
                If CDbl(Request.QueryString.Item("BeginProcess")) = 1 Or Request.Form.Item("mlngOptionalBeginProcess") = vbNullString Then
                    mlngOptionalBeginProcess = 1
                Else
                    mlngOptionalBeginProcess = Request.Form.Item("mlngOptionalBeginProcess")
                End If
                '+ Se procede a mostrar los registros encontrados.                
                Call ShowRecords()
            End If
        End If
	Response.Write(mobjGrid.closeTable())
	
	'+ Se incluyen los botones Back y Next en la página.    
	Response.Write(mobjValues.ButtonBackNext( , mblnDisabledBack, mblnDisabledNext))
	
	mcolQueryClients = Nothing
	mobjGrid = Nothing
End Sub


'% ShowRecords : Muestra los datos contenidos en la colección.
'--------------------------------------------------------------------------------------------
Sub ShowRecords()
	'--------------------------------------------------------------------------------------------
	Dim lintRecordIndex As Integer
	Dim lintRecordShow As Short
	
	lintRecordShow = 0
	
	'+ Estableciendo valores iniciales.    
	mblnDisabledBack = False
	mblnDisabledNext = False
	
	If Request.QueryString.Item("BeginProcess") = vbNullString Then
		'+ Establece el número de página a mostrar.
		If Request.Form.Item("hddPageNumber") = vbNullString Then
			mintPageNumber = 0
		Else
			mintPageNumber = Request.Form.Item("hddPageNumber")
		End If
	Else
		mintPageNumber = 0
	End If
	
	'+ Según el tipo de movimiento realizado se establecen las acciones a tomar
	If Request.QueryString.Item("mstrWay") = vbNullString Or Request.QueryString.Item("mstrWay") = "Next" Then
		mintPageNumber = mintPageNumber + 1
		
	ElseIf Request.QueryString.Item("mstrWay") = "Back" Then 
		mlngOptionalBeginProcess = mlngOptionalBeginProcess - (mlngOptionalBeginProcess - mintFirstRecord)
		mintPageNumber = mintPageNumber - 1
		
		'+ Si el número de la página es menor a cero, se asume que se encuentra en la primera página.
		If mintPageNumber <= 0 Then
			mintPageNumber = 1
		End If
	End If
	'+ Se recorren los elementos a incluir en la tabla.
	For lintRecordIndex = 1 To mcolQueryClients.Count
    'For lintRecordIndex = 0 To mcolQueryClients.Count -1
		With mobjGrid
			.Columns("tcnConsec_grid").DefValue = mlngOptionalBeginProcess
			.Columns("tctClient_grid").DefValue = mcolQueryClients(lintRecordIndex).sClient & "-" & mcolQueryClients(lintRecordIndex).sDigit & " " & mcolQueryClients(lintRecordIndex).sCliename
			.Columns("cbeSex_grid").DefValue = mcolQueryClients(lintRecordIndex).sSexclien
			.Columns("tcdBirthDate_grid").DefValue = CStr(mcolQueryClients(lintRecordIndex).dBirthdat)
			.Columns("cmdAddress_grid").HRefScript = "ShowPopUp('/VTimeNet/Common/SCA001.aspx?sCodispl=SCA101&sOnSeq=2&sClient=" & mcolQueryClients(lintRecordIndex).sClient & "','ShowAddress',500,500,'yes','yes','no','no')"
			.Columns("tctClient_grid").HRefScript = "RecordFound(" & lintRecordIndex & ",'" & Request.Form.Item("ControlName") & "','" & Request.Form.Item("ControlName") & "_Digit" & "','" & Request.Form.Item("ControlClieName") & "')"
			Response.Write(.DoRow)
		End With
		Response.Write("<SCRIPT>insAddQueryClient(""" & mcolQueryClients(lintRecordIndex).sClient & """,""" & mcolQueryClients(lintRecordIndex).sCliename & """,""" & mcolQueryClients(lintRecordIndex).sDigit & """)" & "</" & "Script>")
		
		lintRecordShow = lintRecordShow + 1
		
		'+ Incremento del número de registro total.
		mlngOptionalBeginProcess = mlngOptionalBeginProcess + 1
		
		'+ Verifica si la cantidad de registros mostrados excede el límite establecido en la página.
		If lintRecordIndex >= CN_MAXRECORDS Then
			Exit For
		End If
	Next 
	
	With mobjValues
		'+ Primer registro a cargar    
		Response.Write(.HiddenControl("hddFirstRecord", mintFirstRecord))
		'+ Ultimo registro a cargar        
		Response.Write(.HiddenControl("hddLastRecord", mintLastRecord))
		'+ Indice que indica el primer item a leer de la lista.
		Response.Write(.HiddenControl("mlngOptionalBeginProcess", mlngOptionalBeginProcess))
		'+ Contador de páginas
		Response.Write(.HiddenControl("hddPageNumber", mintPageNumber))
	End With
	
	'+ Determina si estará activo o no el Botón [<< Anterior]                                    
	If mintPageNumber <= 1 Then
		mblnDisabledBack = True
	End If
	
	'+ Determina si estará activo o no el Botón [>> Siguiente]                                    
	If (lintRecordShow < CN_MAXRECORDS) Then
		mblnDisabledNext = True
	End If
End Sub

'% insDefineHeader: se definen los campos del grid
'--------------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'--------------------------------------------------------------------------------------------
	mobjGrid = New eFunctions.Grid
	
	mobjGrid.sCodisplPage = "clientquery"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(40525, "", "tcnConsec_grid", 4, "",  ,  , False)
		Call .AddTextColumn(0, GetLocalResourceObject("tctClient_gridColumnCaption"), "tctClient_grid", 60, "",  , GetLocalResourceObject("tctClient_gridColumnToolTip"))
		Call .AddPossiblesColumn(40524, GetLocalResourceObject("cbeSex_gridColumnCaption"), "cbeSex_grid", "Table18", eFunctions.Values.eValuesType.clngComboType)
		Call .AddDateColumn(40527, GetLocalResourceObject("tcdBirthDate_gridColumnCaption"), "tcdBirthDate_grid", "")
		Call .AddAnimatedColumn(0, GetLocalResourceObject("cmdAddress_gridColumnCaption"), "cmdAddress_grid", "/VTimeNet/images/ShowAddress.png", GetLocalResourceObject("cmdAddress_gridColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "BCC1-1"
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("clientquery")
mobjValues = New eFunctions.Values
mcolQueryClients = New eClient.QueryClients

lobjErrors = New eGeneral.GeneralFunction
mstrAlert = "Err. 1068 " & lobjErrors.insLoadMessage(1068)
lobjErrors = Nothing

mobjValues.sCodisplPage = "clientquery"

%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<%=mobjValues.StyleSheet()%>
	<%=mobjValues.WindowsTitle("BCC1-1")%>
<SCRIPT>
var marrQC = []
var mintCount = -1

//InsValidateFind: Función encargada de enviar mensaje de validación cuando 
//				   no este lleno los ca,pos requeridos
//------------------------------------------------------------------------
function InsValidateFind(){
//------------------------------------------------------------------------
	alert('<%=mstrAlert%>');
}

//%	MoveRecord: Forza a realizar un submit de la forma según el tipo de movimiento
//%         realizado.
//-------------------------------------------------------------------------------------------
function MoveRecord(mstrWay) {
//-------------------------------------------------------------------------------------------
    switch (mstrWay){
        case "Next":
            document.forms[0].action = "ClientQuery.aspx?ApplyCondition=1&mstrWay=Next"
            break;
        case "Back":
            document.forms[0].action = "ClientQuery.aspx?ApplyCondition=1&mstrWay=Back"
    }
    document.forms[0].submit()
}

//%	insAddQueryClient: Carga el arreglo con la consulta obtenida.
//-------------------------------------------------------------------------------------------
function insAddQueryClient(sClient, sClieName, sDigit) {
//-------------------------------------------------------------------------------------------
    var lQueryClient = []
    
    lQueryClient[0]     = sClient
    lQueryClient[1]     = sClieName    
    lQueryClient[2]     = sDigit    
    marrQC[++mintCount] = lQueryClient
}

//%	RecordFound: Retorna el código del cliente seleccionado.
//-------------------------------------------------------------------------------------------
function RecordFound(Field, ControlName, ControlName_Digit, DIVName) {
//-------------------------------------------------------------------------------------------
    var lintIndex = --Field
    var lintError
    if( typeof(DIVName)!='undefined' &&
        DIVName!='')
        UpdateDiv (DIVName, marrQC[lintIndex][1], 'PopUp')
        
	with(opener.document.forms[0]){
		elements[ControlName].value = marrQC[lintIndex][0];
		elements[ControlName + "_Old"].value = marrQC[lintIndex][0];
		elements[ControlName_Digit].value = marrQC[lintIndex][2];
		elements[ControlName + '_Digit_Old'].value = marrQC[lintIndex][2];
		if(typeof(cbePerson_typ)!='undefined')
			cbePerson_typ.value = self.document.forms[0].cbePerson_typ.value;
	}
	<%
If Request.QueryString.Item("sOnChange") <> vbNullString Then
	Response.Write("opener." & Request.QueryString.Item("sOnChange") & ";")
End If
%>
    window.close();
}

//%	InsOptionValue: habilita y deshabilita campos de la busqueda de cliente.
//-------------------------------------------------------------------------------------------
function InsOptionValue() {
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
		if (cbePerson_typ.value==1){
		
				tctClientCode.disabled = false;
				tctClientName.disabled = false;
				tctLastName.disabled   = false;
				tctLastName2.disabled  = false;
				tcdBirthDate.disabled  = false;
				cbeSex.disabled        = false;
// Limpia los valores ya ingresados				
				tctClientCode.value = '';
				tctClientName.value = '';
				tctLastName.value   = '';
				tctLastName2.value  = '';
				tcdBirthDate.value  = '';
				cbeSex.value        = '';
		}		
        else{
				tctClientCode.disabled = false;
				tctClientName.disabled = false;
				tctLastName.disabled   = true;
				tctLastName2.disabled  = true;
				tcdBirthDate.disabled  = false;
				cbeSex.disabled        = true;
// Limpia los valores ya ingresados				
				tctClientCode.value = '';
				tctClientName.value = '';
				tctLastName.value   = '';
				tctLastName2.value  = '';
				tcdBirthDate.value  = '';
				cbeSex.value        = '';
		}
    }
}
//% ValPersontyp() : Deshabilita campos si el cliente es jurídico, una vez que se ha realizado la consulta
//-------------------------------------------------------------------------------------------
function ValPersontyp() {
//-------------------------------------------------------------------------------------------		
with(document.forms[0]){
				tctClientCode.disabled = false;
				tctClientName.disabled = false;
				tctLastName.disabled   = true;
				tctLastName2.disabled  = true;
				tcdBirthDate.disabled  = false;
				cbeSex.disabled        = true;
}								
}
//%	InsPutCero: llena con cero el codigo del cliente
//-------------------------------------------------------------------------------------------
function InsPutCero(sCodClient) {
//-------------------------------------------------------------------------------------------		
	if (sCodClient.value!='')
		self.document.forms[0].tctClientCode.value = InsValuesCero(sCodClient)
}
</SCRIPT>
</HEAD>
<BODY>
<%="<FORM METHOD=POST ACTION=""ClientQuery.aspx?ApplyCondition=1&BeginProcess=1&ControlName=" & Request.Form.Item("ControlName") & "&mstrWay=" & mstrWay & "&sOnChange=" & Replace(Request.QueryString.Item("sOnChange"), """", "'") & """>"%>
        <TABLE ALIGN="CENTER" WIDTH=100%>
            <TR>
                <TD><LABEL ID=0><%= GetLocalResourceObject("cbePerson_typCaption") %></LABEL></TD>            
                <TD><%If IsNothing(Request.Form.Item("cbePerson_typ")) Then
	lintDefvalue = 1
Else
	lintDefvalue = Request.Form.Item("cbePerson_typ")
End If

mobjValues.BlankPosition = 0
Response.Write(mobjValues.PossiblesValues("cbePerson_typ", "Table5006", eFunctions.Values.eValuesType.clngComboType, lintDefvalue,  ,  ,  ,  ,  , "InsOptionValue();",  ,  , GetLocalResourceObject("cbePerson_typToolTip")))
%>
				</TD>
            </TR>
            <TR>
                <TD><BR></TD>            
            </TR>            
            <TR>
                <TD><LABEL ID=40520><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
                <TD><LABEL ID=40522><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
                <TD><LABEL ID=40523><%= GetLocalResourceObject("tctClientCodeCaption") %></LABEL></TD>
            </TR>
            <TR>
                <TD><%=mobjValues.TextControl("tctClientCode", CShort("14"), Request.Form.Item("tctClientCode"),  , GetLocalResourceObject("tctClientCodeToolTip"), False,  ,  , "InsPutCero(this);")%></TD>
                <TD><%=mobjValues.DateControl("tcdBirthDate", Request.Form.Item("tcdBirthDate"), False, GetLocalResourceObject("tcdBirthDateToolTip"))%></TD>
                <TD><%=mobjValues.PossiblesValues("cbeSex", "Table18", 1, Request.Form.Item("cbeSex"),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeSexToolTip"))%></TD>
            </TR>
            <TR>
                <TD><LABEL ID=40521><%= GetLocalResourceObject("Anchor3Caption") %></LABEL></TD>
				<TD><LABEL ID=40521><%= GetLocalResourceObject("Anchor4Caption") %></LABEL></TD>
                <TD><LABEL ID=40521><%= GetLocalResourceObject("tctClientNameCaption") %></LABEL></TD>    
            </TR>
            
            <TR>
                <TD><%=mobjValues.TextControl("tctClientName", CShort("19"), Request.Form.Item("tctClientName"),  , GetLocalResourceObject("tctClientNameToolTip"), False,,,,,,63)%></TD>
				<TD><%=mobjValues.TextControl("tctLastName", CShort("19"), Request.Form.Item("tctLastName"),  , GetLocalResourceObject("tctLastNameToolTip"), False)%></TD>
				<TD><%=mobjValues.TextControl("tctLastName2", CShort("19"), Request.Form.Item("tctLastName2"),  , GetLocalResourceObject("tctLastName2ToolTip"), False)%></TD>
            </TR>
        </TABLE>
<%
'+ Incluye el botón de aceptar y cancelar.
Response.Write(mobjValues.ButtonAcceptCancel( ,  , True))

'+ El control siguiente mantiene el nombre del objeto destino de la selección (Código).
If Not Request.QueryString.Item("ControlName") = vbNullString Then
	mstrControlName = Request.QueryString.Item("ControlName")
Else
	mstrControlName = Request.Form.Item("ControlName")
End If
Response.Write(mobjValues.HiddenControl("ControlName", mstrControlName))

'+ El control siguiente mantiene el nombre del objeto destino de la selección (Nombre).
If Not Request.QueryString.Item("ControlClieName") = vbNullString Then
	mstrControlClieName = Request.QueryString.Item("ControlClieName")
Else
	mstrControlClieName = Request.Form.Item("ControlClieName")
End If
Response.Write(mobjValues.HiddenControl("ControlClieName", mstrControlClieName))

Call insDefineHeader()

If CDbl(Request.QueryString.Item("ApplyCondition")) = 1 Then
	If IsNothing(Request.Form.Item("tctClientCode")) And IsNothing(Request.Form.Item("tctClientName")) And IsNothing(Request.Form.Item("tctLastName")) And IsNothing(Request.Form.Item("tctLastName2")) Then
		Response.Write("<SCRIPT>InsValidateFind()</Script>")
	Else
		Call ApplyCondition()
	End If
End If
%>
    </FORM>
</BODY>
</HTML>
<%
'+Si el tipo de Cliente es jurídico, y ya se ha realizado la búsqueda,
'  se llama a la función que desahabilta los campos 

If lintDefvalue = 2 Then
	Response.Write("<SCRIPT>ValPersontyp();</script>")
End If

%>
<%
mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 17.17.03
Call mobjNetFrameWork.FinishPage("clientquery")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





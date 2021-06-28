<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCoReinsuran" %>
<script language="VB" runat="Server">

'- Cantidad máxima de elementos por página.
Const CN_MAXRECORDS As Short = 20

'- Contador del número de registros insertados en la página
Dim mlngOptionalBeginProcess As Object

'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values

'-Objeto para el manejo y evaluación de las instrucciones SQL para las búsquedas.
Dim mclsCompany As eCoReinsuran.Company

'- Primer y último nombre mostrado en cada página.
Dim mlngFirstRecord As Object
Dim mlngLastRecord As Object

'- Indica el movimiento a efectuar para la búsqueda de los datos. (Next o Previous)    
Dim mstrWay As String

'- Nombre del control destino del código de la selección.
Dim mstrControlName As String

'- Nombre del control destino del nombre de la selección.    
Dim mstrControlCompanyName As String

'- Número de página que se está mostrando
Dim mlngPageNumber As Object

Dim mintCount As Byte
Dim mobjGrid As eFunctions.Grid

'- Habilita o desabilita las acciones sobre los botones Back y Next.
Dim mblnDisabledBack As Boolean
Dim mblnDisabledNext As Boolean



'% ApplyCondition : Ejecuta la consulta según los parámetros dados.
'--------------------------------------------------------------------------------------------
Private Sub ApplyCondition()
	'--------------------------------------------------------------------------------------------
	
	'+ Se inicializan las variables si éstas no poseen valor.
	If mlngFirstRecord = vbNullString Then
		mlngFirstRecord = 1
	End If
	If mlngLastRecord = vbNullString Then
		mlngLastRecord = mlngFirstRecord + CN_MAXRECORDS - 1
	End If
	
	'+ Se inicializa el número de página mostrado.       
	mlngPageNumber = 1
	
	'+ Según el tipo de movimiento realizado se construye la cláusula WHERE especial.
	If Request.QueryString.Item("sWay") = "Next" Then
		mlngFirstRecord = CDbl(Request.Form.Item("hddLastRecord")) + 1
		mlngLastRecord = mlngFirstRecord + CN_MAXRECORDS - 1
	ElseIf Request.QueryString.Item("sWay") = "Back" Then 
		mlngFirstRecord = CDbl(Request.Form.Item("hddFirstRecord")) - CN_MAXRECORDS
		mlngLastRecord = CDbl(Request.Form.Item("hddFirstRecord")) - 1
	End If
	
	'+ Se realiza la búsqueda de los datos.
	If mclsCompany.insPreparedQuery(Request.Form.Item("tctCompanyCode"), Request.Form.Item("tctCompanyName"), Request.Form.Item("cbeType"), CShort(mlngFirstRecord), CShort(mlngLastRecord)) Then
		If mclsCompany.ItemCompany(mintCount) Then
			'+ Se obtiene el número del primer elemento de la página.
			If CDbl(Request.QueryString.Item("BeginProcess")) = 1 Or Request.Form.Item("mlngOptionalBeginProcess") = vbNullString Then
				mlngOptionalBeginProcess = 1
			Else
				mlngOptionalBeginProcess = Request.Form.Item("mlngOptionalBeginProcess")
			End If
		End If
	End If
	
	'+ Se procede a mostrar los registros encontrados.                                
	Call ShowRecords()
	
	Response.Write(mobjGrid.closeTable())
	
	'+ Se incluyen los botones Back y Next en la página.    
	Response.Write(mobjValues.ButtonBackNext( , mblnDisabledBack, mblnDisabledNext))
	
	mclsCompany = Nothing
	mobjGrid = Nothing
End Sub

'% ShowRecords : Muestra los datos contenidos en la colección.
'--------------------------------------------------------------------------------------------
Sub ShowRecords()
	'--------------------------------------------------------------------------------------------
	Dim lintRecordIndex As Integer
	Dim lintRecordShow As Short
	Dim lblnFind As Boolean
	Dim lstrType As String
	
	lintRecordShow = 0
	
	'+ Estableciendo valores iniciales.    
	mblnDisabledBack = False
	mblnDisabledNext = False
	
	If Request.QueryString.Item("BeginProcess") = vbNullString Then
		'+ Establece el número de página a mostrar.
		If Request.Form.Item("hddPageNumber") = vbNullString Then
			mlngPageNumber = 0
		Else
			mlngPageNumber = Request.Form.Item("hddPageNumber")
		End If
	Else
		mlngPageNumber = 0
	End If
	
	'+ Según el tipo de movimiento realizado se establecen las acciones a tomar
	If Request.QueryString.Item("sWay") = vbNullString Or Request.QueryString.Item("sWay") = "Next" Then
		mlngPageNumber = mlngPageNumber + 1
		
	ElseIf Request.QueryString.Item("sWay") = "Back" Then 
		mlngOptionalBeginProcess = mlngOptionalBeginProcess - (mlngOptionalBeginProcess - mlngFirstRecord)
		mlngPageNumber = mlngPageNumber - 1
		
		'+ Si el número de la página es menor a cero, se asume que se encuentra en la primera página.
		If mlngPageNumber <= 0 Then
			mlngPageNumber = 1
		End If
	End If
	
	'+ Se realiza el Find para la carga de los registros                    	
	If CDbl(Request.Form.Item("cbeType")) = 0 Then
		lstrType = vbNullString
	Else
		lstrType = Request.Form.Item("cbeType")
	End If
	
	lblnFind = mclsCompany.insPreparedQuery(Request.Form.Item("tctCompanyCode"), Request.Form.Item("tctCompanyName"), lstrType, CShort(mlngFirstRecord), CShort(mlngLastRecord))
	
	'+ Se recorren los elementos a incluir en la tabla.
	If lblnFind Then
		For lintRecordIndex = 0 To mclsCompany.Count - 1
			If mclsCompany.ItemCompany(lintRecordIndex) Then
				With mobjGrid
					.Columns("tcnCompany").DefValue = CStr(mclsCompany.nCompany)
					.Columns("tctCompanyName").DefValue = CStr(mclsCompany.nCompany)
					.Columns("cbeType").DefValue = mclsCompany.sType
					.Columns("cmdAddress").HRefScript = "ShowPopUp('/VTimeNet/Common/SCA001.aspx?sCodispl=SCA101&sOnSeq=2&sRecType=1&sClient=" & mclsCompany.sClient & "','ShowAddress',750,500,'yes','yes','no','no')"
					.Columns("tctCompanyName").HRefScript = "RecordFound(" & lintRecordIndex & ",'" & Request.Form.Item("ControlName") & "','" & Request.Form.Item("ControlCompanyName") & "')"
					Response.Write(.DoRow)
				End With
				
				Response.Write("<SCRIPT>insAddQueryCompany(""" & mclsCompany.nCompany & """,""" & mclsCompany.sCliename & """)</" & "Script>")
				lintRecordShow = lintRecordShow + 1
				
				'+ Incremento del número de registro total.
				mlngOptionalBeginProcess = mlngOptionalBeginProcess + 1
				
				'+ Verifica si la cantidad de registros mostrados excede el límite establecido en la página.
				If lintRecordIndex >= CN_MAXRECORDS Then
					Exit For
				End If
			End If
		Next 
	End If
	
	With mobjValues
		'+ Primer registro a cargar    
		Response.Write(.HiddenControl("hddFirstRecord", mlngFirstRecord))
		'+ Ultimo registro a cargar        
		Response.Write(.HiddenControl("hddLastRecord", mlngLastRecord))
		'+ Indice que indica el primer item a leer de la lista.
		Response.Write(.HiddenControl("mlngOptionalBeginProcess", mlngOptionalBeginProcess))
		'+ Contador de páginas
		Response.Write(.HiddenControl("hddPageNumber", mlngPageNumber))
	End With
	
	'+ Determina si estará activo o no el Botón [<< Anterior]                                    
	If mlngPageNumber <= 1 Then
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
	
	mobjGrid.sCodisplPage = "companyquery"
	
	'+ Se definen las columnas del grid    
	With mobjGrid.Columns
		Call .AddNumericColumn(0, GetLocalResourceObject("tcnCompanyColumnCaption"), "tcnCompany", 4, CStr(0))
		Call .AddCompanyColumn(0, GetLocalResourceObject("tctCompanyNameColumnCaption"), "tctCompanyName", "",  ,  ,  ,  , "tctCompanyNameDesc")
		Call .AddPossiblesColumn(0, GetLocalResourceObject("cbeTypeColumnCaption"), "cbeType", "Table219", eFunctions.Values.eValuesType.clngComboType)
		Call .AddAnimatedColumn(0, GetLocalResourceObject("cmdAddressColumnCaption"), "cmdAddress", "/VTimeNet/images/ShowAddress.png", GetLocalResourceObject("cmdAddressColumnToolTip"))
	End With
	
	'+ Se definen las propiedades generales del grid
	With mobjGrid
		.Codispl = "SCA111"
		.AddButton = False
		.DeleteButton = False
		.Columns("Sel").GridVisible = False
	End With
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mclsCompany = New eCoReinsuran.Company

mintCount = 0

mobjValues.sCodisplPage = "companyquery"
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT>
//+ Variable para el control de versiones
	document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:02 $"
</SCRIPT>    
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


    <%=mobjValues.StyleSheet()%>
    <%=mobjValues.WindowsTitle("SCA111")%>
<SCRIPT>
var marrQC = []
var mintCount = -1

//%	MoveRecord: Forza a realizar un submit de la forma según el tipo de movimiento
//%         realizado.
//-------------------------------------------------------------------------------------------
function MoveRecord(sWay) {
//-------------------------------------------------------------------------------------------
    switch (sWay){
        case "Next":
            document.forms[0].action = "CompanyQuery.aspx?ApplyCondition=1&sWay=Next"
            break;
        case "Back":
            document.forms[0].action = "CompanyQuery.aspx?ApplyCondition=1&sWay=Back"
    }
    document.forms[0].submit()
}

//%	insAddQueryCompany: Carga el arreglo con la consulta obtenida.
//-------------------------------------------------------------------------------------------
function insAddQueryCompany(sCompany, sCompanyName) {
//-------------------------------------------------------------------------------------------
    var larrQueryCompany = []
    larrQueryCompany[0] = sCompany
    larrQueryCompany[1] = sCompanyName
    marrQC[++mintCount] = larrQueryCompany
}

//%	RecordFound: Retorna el código de la compañía seleccionada.
//-------------------------------------------------------------------------------------------
function RecordFound(lField, sControlName, sDIVName) {
//-------------------------------------------------------------------------------------------
    var llngIndex = lField
    var lerrCatch
    
    if(typeof(sDIVName)!='undefined' &&
       sDIVName!='')
        UpdateDiv(sDIVName, marrQC[llngIndex][1], 'PopUp')
    try {
		with(opener.document.forms[0]){
            elements[sControlName].value = marrQC[llngIndex][0];
			opener.$("#" + sControlName).change();
        }
    }
    catch(lerrCatch){}
    window.close();
}
</SCRIPT>
</HEAD>
<BODY>
<%="<FORM METHOD=POST ACTION=""CompanyQuery.aspx?ApplyCondition=1&BeginProcess=1&ControlName=" & Request.Form.Item("ControlName") & "&sWay=" & mstrWay & """>"%>
        <%=mobjValues.ShowWindowsName("SCA111")%>
        <TABLE WIDTH=100%>
            <TR>
                <TD><LABEL ID=40520><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
                <TD><LABEL ID=40521><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
                <TD><LABEL ID=40522><%= GetLocalResourceObject("tctCompanyCodeCaption") %></LABEL></TD>                
            </TR>
            <TR>
                <TD><%=mobjValues.TextControl("tctCompanyCode", CShort("4"), Request.Form.Item("tctCompanyCode"),  , GetLocalResourceObject("tctCompanyCodeToolTip"), False)%></TD>
                <TD><%=mobjValues.TextControl("tctCompanyName", CShort("50"), Request.Form.Item("tctCompanyName"),  , GetLocalResourceObject("tctCompanyNameToolTip"), False)%></TD>                
                <TD><%=mobjValues.PossiblesValues("cbeType", "Table219", 1, Request.Form.Item("cbeType"))%></TD>
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
If Not Request.QueryString.Item("ControlCompanyName") = vbNullString Then
	mstrControlCompanyName = Request.QueryString.Item("ControlCompanyName")
Else
	mstrControlCompanyName = Request.Form.Item("ControlCompanyName")
End If
Response.Write(mobjValues.HiddenControl("ControlCompanyName", mstrControlCompanyName))

Call insDefineHeader()

If CDbl(Request.QueryString.Item("ApplyCondition")) = 1 Then
	Call ApplyCondition()
End If
%>
    </FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
%>





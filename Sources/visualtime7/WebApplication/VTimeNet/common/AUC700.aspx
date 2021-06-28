<%@ Page Language="VB" Explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon"
    EnableViewState="false" %>

<%@ Import Namespace="eFunctions.Parameter" %>

<%@ Import Namespace="eFunctions.Tables" %>

<%@ Import Namespace="eNetFrameWork" %>
<%@ Import Namespace="eFunctions" %>
<%@ Import Namespace="eClient" %>
<%@ Import Namespace="eGeneral" %>
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
    Dim mstrControlPlaca As String

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
	     Dim sSql As String = "SELECT AU.SCLIENT, AU.NBRANCH, AU.NPRODUCT, AU.NPOLICY, AU.NCERTIF, AU.SREGIST, AU.SMOTOR, AU.SCHASSIS, AU.SCOLOR, MA.SDESCRIPT AS MARCA, AV.SVEHMODEL , TY.SDESCRIPT AS TYPE FROM TABLE226 TY, AUTO AU , TAB_AU_VEH AV,  TABLE7042 MA, CERTIFICAT C  WHERE AU.NVEHTYPE = TY.NVEHTYPE AND AU.SVEHCODE = AV.SVEHCODE AND AV.NVEHBRAND = MA.NVEHBRAND AND AU.DNULLDATE IS NULL AND C.SCERTYPE = AU.SCERTYPE AND C.NBRANCH = AU.NBRANCH AND C.NPOLICY = AU.NPOLICY AND C.NPRODUCT = AU.NPRODUCT AND C.NCERTIF = AU.NCERTIF AND C.SSTATUSVA IN ('1','4','5') "
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
        
        
        Dim rdb As New eRemoteDB.Execute
        
        If Not String.IsNullOrEmpty(Request.Form.Item("tctMotor")) then 
            sSql = sSql + "AND TRIM(AU.SMOTOR) LIKE '" & Request.Form.Item("tctMotor") & "' "
        End If   
        If Not String.IsNullOrEmpty(Request.Form.Item("tctChasis")) then             
            sSql = sSql + "AND TRIM(AU.SCHASSIS) LIKE '" & Request.Form.Item("tctChasis") & "' "
        End If  
        If Not String.IsNullOrEmpty(Request.Form.Item("tctRegister")) and Not String.IsNullOrEmpty(Request.Form.Item("optRegister"))  then 
            sSql = sSql + "AND TRIM(AU.SREGIST) LIKE '" & Request.Form.Item("tctRegister") & "' AND TRIM(NVL(SLICENSE_TY,1)) = " & Request.Form.Item("optRegister")
        End If   
                
               
        rdb.SQL = sSql

        If rdb.Run(True) Then
            Do While Not rdb.EOF
                
                With mobjGrid
                .Columns("tcnConsec_grid").DefValue = IIf(String.IsNullOrEmpty(mlngOptionalBeginProcess), 0,mlngOptionalBeginProcess)
                .Columns("sClient").DefValue =   rdb.FieldToClass("SCLIENT")
               '.Columns("tctClient_grid").DefValue = mcolQueryClients(lintRecordIndex).sClient & "-" & mcolQueryClients(lintRecordIndex).sDigit & " " & mcolQueryClients(lintRecordIndex).sClieName
                .Columns("nBranch").DefValue =   rdb.FieldToClass("NBRANCH")
                .Columns("nProduct").DefValue =   rdb.FieldToClass("NPRODUCT")
                .Columns("nPolicy").DefValue =   rdb.FieldToClass("NPOLICY")    
                .Columns("nCertif").DefValue =   rdb.FieldToClass("NCERTIF")
                .Columns("sRegist").DefValue =   rdb.FieldToClass("SREGIST")
                .Columns("sMotor").DefValue =   rdb.FieldToClass("SMOTOR")
                .Columns("sChassis").DefValue =   rdb.FieldToClass("SCHASSIS")
                .Columns("sColor").DefValue =   rdb.FieldToClass("SCOLOR")
                .Columns("cbeMarca").DefValue = rdb.FieldToClass("MARCA")
                .Columns("sModelo").DefValue =   rdb.FieldToClass("SVEHMODEL")
                .Columns("cbeVehtype").DefValue = rdb.FieldToClass("TYPE")
                .Columns("sRegist").HRefScript = "RecordFound( 'tctRegister' , 'cbeBranch', 'valProduct', 'tcnPolicy', 'tcnCertificat' ,'" & .Columns("sRegist").DefValue & "','" & .Columns("nBranch").DefValue & "','" &  .Columns("nProduct").DefValue & "','" &  .Columns("nPolicy").DefValue & "','" &  .Columns("nCertif").DefValue  & "' )" 
                Response.Write(.DoRow)
            End With
                   
                
                lintRecordShow = lintRecordShow + 1
		
                '+ Incremento del número de registro total.
                mlngOptionalBeginProcess = mlngOptionalBeginProcess + 1
		
                '+ Verifica si la cantidad de registros mostrados excede el límite establecido en la página.
                If lintRecordIndex >= CN_MAXRECORDS Then
                    'Exit For
                End If
                rdb.RNext()
            Loop   
            rdb.RCloseRec()             
        End If
        
                With mobjValues
             'Primer registro a cargar    
            Response.Write(.HiddenControl("hddFirstRecord", mintFirstRecord))
             'Ultimo registro a cargar        
            Response.Write(.HiddenControl("hddLastRecord", mintLastRecord))
             'Indice que indica el primer item a leer de la lista.
            Response.Write(.HiddenControl("mlngOptionalBeginProcess", mlngOptionalBeginProcess))
             'Contador de páginas
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
        
        Response.Write(mobjGrid.closeTable())
	
        '+ Se incluyen los botones Back y Next en la página.    
        Response.Write(mobjValues.ButtonBackNext(, mblnDisabledBack, mblnDisabledNext))
	
        mcolQueryClients = Nothing
        mobjGrid = Nothing
    End Sub


    '% ShowRecords : Muestra los datos contenidos en la colección.
    '--------------------------------------------------------------------------------------------
    'Sub ShowRecords()
    '    '--------------------------------------------------------------------------------------------
        
    '    '+ Se recorren los elementos a incluir en la tabla.
    '    For lintRecordIndex = 1 To mcolQueryClients.Count
    '        'For lintRecordIndex = 0 To mcolQueryClients.Count -1
            
    '        Response.Write("<script>insAddQueryClient(""" & mcolQueryClients(lintRecordIndex).sClient & """,""" & mcolQueryClients(lintRecordIndex).sClieName & """,""" & mcolQueryClients(lintRecordIndex).sDigit & """)" & "</" & "Script>")

    '    Next
	

    'End Sub

    '% insDefineHeader: se definen los campos del grid
    '--------------------------------------------------------------------------------------------
    Private Sub insDefineHeader()
        '--------------------------------------------------------------------------------------------
        mobjGrid = New eFunctions.Grid
	
        mobjGrid.sCodisplPage = "auc700"
	
        '+ Se definen las columnas del grid    
        With mobjGrid.Columns
            Call .AddNumericColumn(40525, "", "tcnConsec_grid", 4, 1, , , False)
            Call .AddTextColumn(0, GetLocalResourceObject("sclientCaption"), "sClient", 60, "", , GetLocalResourceObject("sclientCaption"))
            Call .AddNumericColumn(40525, GetLocalResourceObject("nbranchCaption"), "nBranch", 4, "", , GetLocalResourceObject("nbranchCaption"), False)
            Call .AddNumericColumn(40525, GetLocalResourceObject("nproductCaption"), "nProduct", 4, "", , GetLocalResourceObject("nproductCaption"), False)
            Call .AddNumericColumn(40525, GetLocalResourceObject("npolicyCaption"), "nPolicy", 4, "", , GetLocalResourceObject("npolicyCaption"), False)
            Call .AddNumericColumn(40525, GetLocalResourceObject("ncertifCaption"), "nCertif", 4, "", , GetLocalResourceObject("ncertifCaption"), False)
            Call .AddTextColumn(0, GetLocalResourceObject("sregistCaption"), "sRegist", 60, "", , GetLocalResourceObject("sregistCaption"))
            Call .AddTextColumn(0, GetLocalResourceObject("smotorCaption"), "sMotor", 60, "", , GetLocalResourceObject("smotorCaption"))
            Call .AddTextColumn(0, GetLocalResourceObject("schassisCaption"), "sChassis", 60, "", , GetLocalResourceObject("schassisCaption"))
            Call .AddTextColumn(0, GetLocalResourceObject("scolorCaption"), "sColor", 60, "", , GetLocalResourceObject("scolorCaption"))
            Call .AddTextColumn(40524, GetLocalResourceObject("cbemarcaCaption"), "cbeMarca", 60 , "", ,GetLocalResourceObject("cbemarcaCaption"))
            Call .AddTextColumn(0, GetLocalResourceObject("smodeloCaption"), "sModelo", 60, "", , GetLocalResourceObject("smodeloCaption"))
            Call .AddTextColumn(40524, GetLocalResourceObject("cbevehtypeCaption"), "cbeVehtype", 60, "", ,GetLocalResourceObject("cbevehtypeCaption"))
        End With
	    
        '+ Se definen las propiedades generales del grid 
        With mobjGrid
            .Codispl = "auc700"
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
    Call mobjNetFrameWork.BeginPage("auc700")
    mobjValues = New eFunctions.Values
    mcolQueryClients = New eClient.QueryClients

    lobjErrors = New eGeneral.GeneralFunction
    mstrAlert = "Err. 1068 " & lobjErrors.insLoadMessage(1068)
    lobjErrors = Nothing

    mobjValues.sCodisplPage = "auc700"

%>
<html>
<head>
    <script type="text/javascript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
    <meta name="GENERATOR" content="Microsoft Visual Studio 6.0" />
    <%=mobjValues.StyleSheet()%>
    <%=mobjValues.WindowsTitle("AUC700")%>
    <script>
var marrQC = new Array(0)
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
            document.forms[0].action = "auc700.aspx?ApplyCondition=1&mstrWay=Next"
            break;
        case "Back":
            document.forms[0].action = "auc700.aspx?ApplyCondition=1&mstrWay=Back"
    }
    document.forms[0].submit()
}

//%	insAddQueryClient: Carga el arreglo con la consulta obtenida.
//-------------------------------------------------------------------------------------------
function insAddQueryClient(sClient, sClieName, sDigit) {
//-------------------------------------------------------------------------------------------
    var lQueryClient = new Array(2)
    
    lQueryClient[0]     = sClient
    lQueryClient[1]     = sClieName    
    lQueryClient[2]     = sDigit    
    marrQC[++mintCount] = lQueryClient
}

//%	RecordFound: Retorna el código del cliente seleccionado.  póliza y certificado
//-------------------------------------------------------------------------------------------
function RecordFound(tctRegister_Field, cbeBranch_Field, valProduct_Field, tcnPolicy_Field, tcnCertificat_Field, tctRegister_Value, cbeBranch_Value, valProduct_Value, tcnPolicy_Value, tcnCertificat_Value) {
//------------------------------------------------------------------------------------------- 

     if (tctRegister_Field != "undefined") {
	    with(opener.document.forms[0]) {            
            elements[tctRegister_Field].value = tctRegister_Value;
            elements[cbeBranch_Field].value = cbeBranch_Value;
            elements[valProduct_Field].value = valProduct_Value;
            elements[tcnPolicy_Field].value = tcnPolicy_Value;
            elements[tcnCertificat_Field].value = tcnCertificat_Value;
            //+ Las dos lineas siguientes fueron agregadas para que se llenen todos los campos de la SI001 al cerrar este popup
            elements[tcnPolicy_Field].onblur(); 
            elements[tctRegister_Field].onblur();
	    }
    }
    else {
        alert("Error: valor de placa vacio.")
    }
	<%
If Request.QueryString.Item("sOnChange") <> vbNullString Then
	Response.Write("opener." & Request.QueryString.Item("sOnChange") & ";")
End If
%>
    window.close();
}
    </script>
</head>
<body>
    <%="<FORM METHOD=POST ACTION=""auc700.aspx?ApplyCondition=1&BeginProcess=1&ControlPlaca=" & Request.Form.Item("ControlPlaca") & "&mstrWay=" & mstrWay & "&sOnChange=" & Replace(Request.QueryString.Item("sOnChange"), """", "'") & """>"%>
    <table align="CENTER" width="100%">
        <tr>
            <td>
                <label id="0">
                    <%= GetLocalResourceObject("optRegister_Caption") %></label>
            </td>
            <td>
                        <%Response.Write(mobjValues.OptionControl(40670, "optRegister", GetLocalResourceObject("optRegister_1Caption"), "1" , "1",,,,GetLocalResourceObject("optRegister_1ToolTip")))%>
            </td>
            <td>
                        <%Response.Write(mobjValues.OptionControl(40671, "optRegister", GetLocalResourceObject("optRegister_2Caption"), , "2",,,,GetLocalResourceObject("optRegister_2ToolTip")))%>
            </td>
            <td>
                        <%Response.Write(mobjValues.OptionControl(40672, "optRegister", GetLocalResourceObject("optRegister_3Caption"), , "3",,,,GetLocalResourceObject("optRegister_3ToolTip")))%>
            </td>
            <td>
                <label id="Label1">
                    <%= GetLocalResourceObject("tctRegister_Caption") %></label>
            </td>
             <td>
                <%=mobjValues.TextControl("tctRegister", CShort("14"), , , GetLocalResourceObject("tctRegister_ToolTip"), False)%>
            </td>
        </tr>
        <tr>
            <td>
                <br>
            </td>
        </tr>
        <tr>
            <td>
                <label id="40523">
                    <%= GetLocalResourceObject("tctMotorCaption") %></label>
            </td>
            <td>
                <%= mobjValues.TextControl("tctMotor", CShort("40"), , , GetLocalResourceObject("tctMotor_ToolTip"), False)%>
            </td>
        </tr>
        <tr>
            <td>
                <label id="Label2">
                    <%= GetLocalResourceObject("tctChasisCaption") %></label>
            </td>
            <td>
                <%=mobjValues.TextControl("tctChasis", CShort("40"), , , GetLocalResourceObject("tctChasis_ToolTip"), False)%>
            </td>
        </tr>
    </table>
    <%
        '+ Incluye el botón de aceptar y cancelar.
        Response.Write(mobjValues.ButtonAcceptCancel(, , True))

        '+ El control siguiente mantiene el nombre del objeto destino de la selección (Código).
        If Not Request.QueryString.Item("ControlPlaca") = vbNullString Then
            mstrControlPlaca = Request.QueryString.Item("ControlPlaca")
        Else
            mstrControlPlaca = Request.Form.Item("ControlPlaca")
        End If
        Response.Write(mobjValues.HiddenControl("ControlPlaca", mstrControlPlaca))

        ''+ El control siguiente mantiene el nombre del objeto destino de la selección (Nombre).
        'If Not Request.QueryString.Item("ControlClieName") = vbNullString Then
        '    mstrControlClieName = Request.QueryString.Item("ControlClieName")
        'Else
        '    mstrControlClieName = Request.Form.Item("ControlClieName")
        'End If
        'Response.Write(mobjValues.HiddenControl("ControlClieName", mstrControlClieName))

        Call insDefineHeader()

        If CDbl(Request.QueryString.Item("ApplyCondition")) = 1 Then
            If IsNothing(Request.Form.Item("tctRegister")) And IsNothing(Request.Form.Item("tctMotor")) And IsNothing(Request.Form.Item("tctChasis")) And IsNothing(Request.Form.Item("optRegister")) Then
                Response.Write("<script>InsValidateFind()</script>")
            Else
                Call ApplyCondition()
            End If
        End If
    %>
    </form>
</body>
</html>
<%
    mobjValues = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 17.17.03
    Call mobjNetFrameWork.FinishPage("auc700")
    mobjNetFrameWork = Nothing
    '^End Footer Block VisualTimer%>
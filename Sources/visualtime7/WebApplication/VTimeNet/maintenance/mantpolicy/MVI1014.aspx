<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import Namespace="System.Globalization" %>
<%@ Import namespace="eFunctions" %>
<%@ Import Namespace="InMotionGIT.Common.Helpers" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="eFunctions.Values" %>
<%@ Import namespace="eRemoteDB.Parameter" %>

<script language="VB" runat="Server">

	'- Objeto para el manejo de las funciones generales de carga de valores
	Private mobjValues As New eFunctions.Values
	
	'- Se define la variable mobjGrid para el manejo del Grid de la ventana
	Private mobjGrid As eFunctions.Grid

    ''' <summary>
    ''' Definición de columnas del GRID
    ''' </summary>
    Private Sub insDefineHeader()
        'Aqui se puede realizar la visualizacion  del proximo numero a utilizar
        
        Dim rdb As New eRemoteDB.Execute
               
        rdb.SQL = "SELECT NVL(MAX(NID),0) NMAXID  FROM INSUDB.TAR_TRALIFE TAR_TRALIFE  WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND NTYPERISK = :NTYPERISK AND DEFFECDATE <= :DEFFECDATE AND  ( TAR_TRALIFE.DNULLDATE IS NULL OR DNULLDATE > :DNULLDATE0 ) "
        rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(Request.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
        rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(Request.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
        rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(IIf(String.IsNullOrEmpty(Request.QueryString.Item("NMODULEC")), 0, Request.QueryString.Item("NMODULEC")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
        rdb.Parameters.Add("NCOVER", mobjValues.StringToType(Request.QueryString.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
        rdb.Parameters.Add("NTYPERISK", mobjValues.StringToType(IIf(String.IsNullOrEmpty(Request.QueryString.Item("NTYPERISK")), 7, Request.QueryString.Item("NTYPERISK")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 2, 0, 2, eRmtDataAttrib.rdbParamNullable)
        rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
        rdb.Parameters.Add("DNULLDATE0", mobjValues.StringToType(Request.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
        
        Dim NMAXID As Integer
        If rdb.Run(True) Then
            Do While Not rdb.EOF
                NMAXID = rdb.FieldToClass("NMAXID")
                NMAXID += 1
                rdb.RNext()
            Loop
        End If
        
        'Fin de captura del proximo digito 
        
        mobjGrid = New eFunctions.Grid
			
        '+Se definen todas las columnas del Grid
        With mobjGrid.Columns
            .AddNumericColumn(Id:=0, Title:=GetLocalResourceObject("NID_Caption"), FieldName:="NID", Length:=10, DefValue:=nMAXID, isRequired:=True, Alias_Renamed:=GetLocalResourceObject("NID_ToolTip"), ShowThousand:=False, DecimalPlaces:=0, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=GetLocalResourceObject("NAGE_Caption"), FieldName:="NAGE", Length:=5, DefValue:="", isRequired:=False, Alias_Renamed:=GetLocalResourceObject("NAGE_ToolTip"), ShowThousand:=False, DecimalPlaces:=0, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=GetLocalResourceObject("NINIPERCOV_Caption"), FieldName:="NINIPERCOV", Length:=5, DefValue:="", isRequired:=False, Alias_Renamed:=GetLocalResourceObject("NINIPERCOV_ToolTip"), ShowThousand:=False, DecimalPlaces:=0, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=GetLocalResourceObject("NENDPERCOV_Caption"), FieldName:="NENDPERCOV", Length:=5, DefValue:="", isRequired:=False, Alias_Renamed:=GetLocalResourceObject("NENDPERCOV_ToolTip"), ShowThousand:=False, DecimalPlaces:=0, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=GetLocalResourceObject("NINIPAYCOV_Caption"), FieldName:="NINIPAYCOV", Length:=5, DefValue:="", isRequired:=False, Alias_Renamed:=GetLocalResourceObject("NINIPAYCOV_ToolTip"), ShowThousand:=False, DecimalPlaces:=0, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=GetLocalResourceObject("NENDPAYCOV_Caption"), FieldName:="NENDPAYCOV", Length:=5, DefValue:="", isRequired:=False, Alias_Renamed:=GetLocalResourceObject("NENDPAYCOV_ToolTip"), ShowThousand:=False, DecimalPlaces:=0, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=GetLocalResourceObject("NRATEWOMEN_Caption"), FieldName:="NRATEWOMEN", Length:=9, DefValue:="", isRequired:=False, Alias_Renamed:=GetLocalResourceObject("NRATEWOMEN_ToolTip"), ShowThousand:=False, DecimalPlaces:=6, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=GetLocalResourceObject("NPREMWOMEN_Caption"), FieldName:="NPREMWOMEN", Length:=18, DefValue:="", isRequired:=False, Alias_Renamed:=GetLocalResourceObject("NPREMWOMEN_ToolTip"), ShowThousand:=False, DecimalPlaces:=6, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=GetLocalResourceObject("NRATEMEN_Caption"), FieldName:="NRATEMEN", Length:=9, DefValue:="", isRequired:=False, Alias_Renamed:=GetLocalResourceObject("NRATEMEN_ToolTip"), ShowThousand:=False, DecimalPlaces:=6, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=GetLocalResourceObject("NPREMMEN_Caption"), FieldName:="NPREMMEN", Length:=18, DefValue:="", isRequired:=False, Alias_Renamed:=GetLocalResourceObject("NPREMMEN_ToolTip"), ShowThousand:=False, DecimalPlaces:=6, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddPossiblesColumn(Id:=0, Title:=GetLocalResourceObject("NTYPE_TAR_Caption"), FieldName:="NTYPE_TAR", TableName:="TABLE5584", ValuesType:=eValuesType.clngComboType, DefValue:="", NeedParam:=False, ComboSize:="1", OnChange:="InputOnChange(this)", Disabled:=False, MaxLength:=5, Alias_Renamed:=GetLocalResourceObject("NTYPE_TAR_ToolTip"), CodeType:=eTypeCode.eNumeric, bAllowInvalid:=False, ShowDescript:=True, Descript:="", NotCache:=False, KeyField:="")
            .AddNumericColumn(Id:=0, Title:=GetLocalResourceObject("NRATE_Caption"), FieldName:="NRATE", Length:=9, DefValue:="", isRequired:=False, Alias_Renamed:=GetLocalResourceObject("NRATE_ToolTip"), ShowThousand:=False, DecimalPlaces:=6, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddNumericColumn(Id:=0, Title:=GetLocalResourceObject("NPREMIUM_Caption"), FieldName:="NPREMIUM", Length:=18, DefValue:="", isRequired:=False, Alias_Renamed:=GetLocalResourceObject("NPREMIUM_ToolTip"), ShowThousand:=False, DecimalPlaces:=6, OnChange:="InputOnChange(this)", Disabled:=False, bAllowNegativ:=False)
            .AddHiddenColumn(FieldName:="dEffecdateCurrent", DefValue:="""")
        End With
		
        With mobjGrid
            .nMainAction = CShort(IIf(Request.QueryString.Item("nMainAction") = "", 0, Request.QueryString.Item("nMainAction")))
            .Codispl = "MVI1014"
            .Codisp = "MVI1014"
            .Top = 100
            .Height = 450
            .Width = 550
            .ActionQuery = mobjValues.ActionQuery
            .bOnlyForQuery = Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery)
            .Columns("Sel").GridVisible = Not .ActionQuery
            .Columns("NID").EditRecord = True
					
            .Columns("NID").Disabled = (Request.QueryString.Item("Action") = "Update")
			    
            .sDelRecordParam = "cbeBranch=" & Request.QueryString.Item("cbeBranch") + "&valProduct=" & Request.QueryString.Item("valProduct") + "&NMODULEC=" & Request.QueryString.Item("NMODULEC") + "&NCOVER=" & Request.QueryString.Item("NCOVER") + "&NTYPERISK=" & Request.QueryString.Item("NTYPERISK") + "&DEFFECDATE=" & Request.QueryString.Item("DEFFECDATE") & "&NID=' + marrArray[lintIndex].NID + '" & "&dEffecdateCurrent=' + marrArray[lintIndex].dEffecdateCurrent + '"
            .sEditRecordParam = "cbeBranch=" & Request.QueryString.Item("cbeBranch") + "&valProduct=" & Request.QueryString.Item("valProduct") + "&NMODULEC=" & Request.QueryString.Item("NMODULEC") + "&NCOVER=" & Request.QueryString.Item("NCOVER") + "&NTYPERISK=" & Request.QueryString.Item("NTYPERISK") + "&DEFFECDATE=" & Request.QueryString.Item("DEFFECDATE")
					
            If Request.QueryString.Item("Reload") = "1" Then
                .sReloadIndex = Request.QueryString.Item("ReloadIndex")
            End If
					
            .AddButton = True
            .DeleteButton = True
            .Columns("Sel").GridVisible = .DeleteButton

        End With
    End Sub

    ''' <summary>
    ''' Esta función se encarga de cargar los datos en la forma "Folder" 
    ''' </summary>
		Private Sub insPreMVI1014()
        With Request
            Dim rdb As New eRemoteDB.Execute
               
            rdb.SQL = "SELECT  TAR_TRALIFE.NBRANCH, TAR_TRALIFE.NPRODUCT, TAR_TRALIFE.NMODULEC, TAR_TRALIFE.NCOVER, TAR_TRALIFE.DEFFECDATE, TAR_TRALIFE.NTYPERISK, TAR_TRALIFE.NID, TAR_TRALIFE.NAGE, TAR_TRALIFE.NINIPERCOV, TAR_TRALIFE.NENDPERCOV, TAR_TRALIFE.NINIPAYCOV, TAR_TRALIFE.NENDPAYCOV, TAR_TRALIFE.NRATEWOMEN, TAR_TRALIFE.NPREMWOMEN, TAR_TRALIFE.NRATEMEN, TAR_TRALIFE.NPREMMEN, TAR_TRALIFE.DNULLDATE, TAR_TRALIFE.NTYPE_TAR, TAR_TRALIFE.NRATE, TAR_TRALIFE.NPREMIUM FROM INSUDB.TAR_TRALIFE TAR_TRALIFE  WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND NTYPERISK = :NTYPERISK AND DEFFECDATE <= :DEFFECDATE AND  ( TAR_TRALIFE.DNULLDATE IS NULL OR DNULLDATE > :DNULLDATE0 ) "
            rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
            rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
            rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NMODULEC")), 0, .QueryString.Item("NMODULEC")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
            rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.QueryString.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
            rdb.Parameters.Add("NTYPERISK", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NTYPERISK")), 7, .QueryString.Item("NTYPERISK")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 2, 0, 2, eRmtDataAttrib.rdbParamNullable)
            rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
            rdb.Parameters.Add("DNULLDATE0", mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)

            With mobjGrid
                If rdb.Run(True) Then
                    Do While Not rdb.EOF
                        .Columns("NID").DefValue = rdb.FieldToClass("NID")
                        .Columns("NAGE").DefValue = rdb.FieldToClass("NAGE")
                        .Columns("NINIPERCOV").DefValue = rdb.FieldToClass("NINIPERCOV")
                        .Columns("NENDPERCOV").DefValue = rdb.FieldToClass("NENDPERCOV")
                        .Columns("NINIPAYCOV").DefValue = rdb.FieldToClass("NINIPAYCOV")
                        .Columns("NENDPAYCOV").DefValue = rdb.FieldToClass("NENDPAYCOV")
                        .Columns("NRATEWOMEN").DefValue = rdb.FieldToClass("NRATEWOMEN")
                        .Columns("NPREMWOMEN").DefValue = rdb.FieldToClass("NPREMWOMEN")
                        .Columns("NRATEMEN").DefValue = rdb.FieldToClass("NRATEMEN")
                        .Columns("NPREMMEN").DefValue = rdb.FieldToClass("NPREMMEN")

                        .Columns("NTYPE_TAR").DefValue = rdb.FieldToClass("NTYPE_TAR")
                        .Columns("NRATE").DefValue = rdb.FieldToClass("NRATE")
                        .Columns("NPREMIUM").DefValue = rdb.FieldToClass("NPREMIUM")
                        .Columns("dEffecdateCurrent").DefValue = rdb.FieldToClass("dEffecdate")
                        '.Columns("SSMOKING").DefValue = rdb.FieldToClass("SSMOKING")
 
                        Response.Write(.DoRow)
                        rdb.RNext()
                    Loop
                    rdb.RCloseRec()
                End If
                Response.Write(.closeTable())
            End With
        End With
	  End Sub

    ''' <summary>
    ''' Se define esta funcion para contruir el contenido de la ventana UPD de los archivos de datos particulares
    ''' </summary>
		Private Sub insPreMVI1014Upd()
				With Request
					If .QueryString.Item("Action") = "Del" Then						
               	Dim lblnPost As Boolean
                Dim lstrMessage As String = String.Empty
               	Dim rdb As New eRemoteDB.Execute
                
                
                 If String.IsNullOrEmpty(lstrMessage) Then
                    If mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate) = mobjValues.StringToType(.QueryString.Item("dEffecdateCurrent"), eFunctions.Values.eTypeData.etdDate) Then

                        rdb = New eRemoteDB.Execute
                        rdb.SQL = "DELETE FROM INSUDB.TAR_TRALIFE WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND NTYPERISK = :NTYPERISK AND NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND DEFFECDATE = :DEFFECDATE AND NTYPERISK = :NTYPERISK AND NID = :NID "
    
                        rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NMODULEC")), 0, .QueryString.Item("NMODULEC")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.QueryString.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NTYPERISK", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NTYPERISK")), 7, .QueryString.Item("NTYPERISK")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 2, 0, 2, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NID", mobjValues.StringToType(.QueryString.Item("NID"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)

                                       
                        lblnPost = rdb.Run(False)
                    Else

                        rdb = New eRemoteDB.Execute
                        rdb.SQL = "UPDATE INSUDB.TAR_TRALIFE SET DNULLDATE = :DNULLDATE, DCOMPDATE = SYSDATE, NUSERCODE = :NUSERCODE WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND NTYPERISK = :NTYPERISK AND NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND DEFFECDATE = :DEFFECDATE AND NTYPERISK = :NTYPERISK AND NID = :NID "
    
                        rdb.Parameters.Add("DNULLDATE", mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NUSERCODE", Session("NUSERCODE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NMODULEC")), 0, .QueryString.Item("NMODULEC")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.QueryString.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NTYPERISK", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NTYPERISK")), 7, .QueryString.Item("NTYPERISK")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 2, 0, 2, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("dEffecdateCurrent"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NID", mobjValues.StringToType(.QueryString.Item("NID"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
                        
                        

                                       
                        lblnPost = rdb.Run(False)
                    End If

                     Response.Write(mobjValues.ConfirmDelete())
                 Else
                     Response.Write(lstrMessage)
                 End If
		      End If
					
					Response.Write(mobjGrid.DoFormUpd(.QueryString.Item("Action"), "MVI1014_val.aspx", .QueryString.Item("sCodispl"), .QueryString.Item("nMainAction"), mobjGrid.ActionQuery, CShort(.QueryString.Item("Index"))))
				End With		
		End Sub

</script>
<%      
	Response.Expires = -1441
%>
<script type="text/javascript" language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<script type="text/javascript" language="JavaScript">
    function InputOnChange(field) {
    
    }
</script>
<html>
<head>
    <title></title>
		<%
				mobjValues.ActionQuery = (Request.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
		    Response.Write(mobjValues.StyleSheet())
		    Response.Write("<script language='JavaScript'>var	nMainAction	= " & CShort("0" & Request.QueryString.Item("nMainAction")) & "</script>")
		    If Request.QueryString.Item("Type") <> "PopUp" Then
		        With New eFunctions.Menues
		        		Response.Write(.setZone(2, "MVI1014", "MVI1014.aspx"))
		        End With
		    End If
		%>
</head>
<body onunload="closeWindows();">
    <form method="post" id="FORM" action="MVI1014_val.aspx?sZone=2">
		<%
				Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
				Call insDefineHeader()
				If Request.QueryString.Item("Type") <> "PopUp" Then
				    Call insPreMVI1014()
				Else
				    Call insPreMVI1014Upd()
				End If
		%>	  
    </form>
</body>
</html>
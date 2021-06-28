<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import Namespace="System.Globalization" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eRemoteDB.Parameter" %>

<script language="VB" runat="Server">
    Private mstrErrors As String
    Private mobjValues As eFunctions.Values
    Private mstrString As String

    '+ Se define la contante para el manejo de errores en caso de advertencias
    Private mstrCommand As String

    ''' <summary>
    ''' Se realizan las validaciones masivas de la forma
    ''' </summary>
    Function insValidation() As String
        Dim result As String = String.Empty
        With Request
            Select Case .QueryString.Item("sCodispl")
                Case "MAU7000"
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        Dim lclsErrors As New eFunctions.Errors

                        '+ Debe indicarse el ramo
                        If String.IsNullOrEmpty(.Form.Item("cbeBranch")) OrElse .Form.Item("cbeBranch") = "0" Then
                            lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 9064)
                        ElseIf .Form.Item("cbeBranch") <> "6" Then
                            lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 900020)
                        End If
                        '+ Debe indicarse el producto
                        If String.IsNullOrEmpty(.Form.Item("valProduct")) OrElse .Form.Item("valProduct") = "0" Then
                            lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 1014)
                        End If
                        '+ Debe indicarse la fecha de efecto
                        If String.IsNullOrEmpty(.Form.Item("DEFFECTDATE")) Then
                            lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 4003)
                        Else
                            If Request.QueryString.Item("nMainAction") <> 401 Then
                                Dim rdb As New eRemoteDB.Execute
                                Dim rdb2 As New eRemoteDB.Execute
                                Dim ldtmEffecDate As Date
                                Dim ldtmNullDate As Date

                                rdb = New eRemoteDB.Execute
                                rdb.SQL = "SELECT MAX(DEFFECTDATE) AS MAXDEFFECDATE FROM ISSUECONTROLS ISSUECONTROLS WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT"
                                rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)

                                If rdb.Run(True) Then
                                    ldtmEffecDate = mobjValues.StringToType(rdb.FieldToClass("MAXDEFFECDATE"), Values.eTypeData.etdDate)
                                    rdb.RCloseRec()
                                End If

                                rdb2 = New eRemoteDB.Execute
                                rdb2.SQL = "SELECT MAX(DNULLDATE) AS MAXDNULLDATE FROM ISSUECONTROLS ISSUECONTROLS WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT"
                                rdb2.Parameters.Add("NBRANCH", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                rdb2.Parameters.Add("NPRODUCT", mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)

                                If rdb2.Run(True) Then
                                    ldtmNullDate = mobjValues.StringToType(rdb2.FieldToClass("MAXDNULLDATE"), Values.eTypeData.etdDate)
                                    If ldtmNullDate > ldtmEffecDate Then
                                        ldtmEffecDate = ldtmNullDate
                                    End If
                                    rdb2.RCloseRec()
                                End If

                                '+ La fecha debe ser mayor o igual a la última modificación
                                If .Form.Item("DEFFECTDATE") < ldtmEffecDate Then
                                    lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 10869, , , "(" & ldtmEffecDate & ")")
                                End If
                            End If
                        End If

                        result = lclsErrors.Confirm
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            Dim lclsErrors As New eFunctions.Errors
                            Dim lintConsec As System.Int32
                            Dim rdb As New eRemoteDB.Execute

                            '+ Debe indicarse el tipo de vehículo
                            If String.IsNullOrEmpty(.Form.Item("NVEHTYPE")) OrElse .Form.Item("NVEHTYPE") = "0" Then
                                lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 13988)
                            End If
                            '+ Debe indicarse el uso del vehículo
                            If String.IsNullOrEmpty(.Form.Item("NUSE")) OrElse .Form.Item("NUSE") = "0" Then
                                lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 3112)
                            End If
                            '+ Si indica el monto asegurado mínimo no debe indicar antigüedad
                            If Not String.IsNullOrEmpty(.Form.Item("NCAPITALMIN")) And Not String.IsNullOrEmpty(.Form.Item("NOLDEST")) Then
                                lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 900102)
                            Else
                                '+ Si se indica monto asegurado, debe indicar la moneda
                                If Not String.IsNullOrEmpty(.Form.Item("NCAPITALMIN")) And (String.IsNullOrEmpty(.Form.Item("NCURRENCY")) Or .Form.Item("NCURRENCY") = "0") Then
                                    lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 750024)
                                End If
                            End If

                            rdb = New eRemoteDB.Execute
                            rdb.SQL = "SELECT ISSUECONTROLS.NCONSEC FROM ISSUECONTROLS ISSUECONTROLS WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NVEHTYPE = :NVEHTYPE AND NUSE = :NUSE AND DEFFECTDATE <= :DEFFECTDATE AND (DNULLDATE  IS NULL OR  DNULLDATE  > :DEFFECTDATE)"
                            rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NVEHTYPE", mobjValues.StringToType(.Form.Item("NVEHTYPE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NUSE", mobjValues.StringToType(.Form.Item("NUSE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("DEFFECTDATE", mobjValues.StringToType(.QueryString.Item("DEFFECTDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)

                            If rdb.Run(True) Then
                                lintConsec = rdb.FieldToClass("NCONSEC")
                                rdb.RCloseRec()
                            End If
                            '+ No debe existir el registro en la tabla
                            If Request.QueryString.Item("Action") = "Add" And lintConsec > 0 Then
                                lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 900115)
                            End If
                            If Request.QueryString.Item("Action") = "Update" And lintConsec > 0 And lintConsec <> .Form.Item("nConsecGrid") Then
                                lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 900115)
                            End If

                            result = lclsErrors.Confirm
                        End If
                    End If

                Case Else
                    result = "insValMAU7000: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
            End Select
        End With
        Return result
    End Function


    ''' <summary>
    ''' Se realizan las actualizaciones a las tablas
    ''' </summary>
    Function insPosting() As Boolean
        Dim lblnPost As Boolean = False
        Dim rdb As eRemoteDB.Execute

        With Request
            Select Case Request.QueryString.Item("sCodispl")
                Case "MAU7000"
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrString = "&cbeBranch=" & .Form.Item("cbeBranch")  &"&valProduct=" & .Form.Item("valProduct")  &"&DEFFECTDATE=" & .Form.Item("DEFFECTDATE")
                        lblnPost = True

                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            mstrString = "&cbeBranch=" & .QueryString.Item("cbeBranch")  &"&valProduct=" & .QueryString.Item("valProduct")  &"&DEFFECTDATE=" & .QueryString.Item("DEFFECTDATE")

                            Select Case .QueryString.Item("Action")
                                Case "Add"
                                    rdb = New eRemoteDB.Execute
                                    rdb.SQL = "INSERT INTO ISSUECONTROLS (NVEHTYPE, NUSE, NOLDEST, NCAPITALMIN, NCURRENCY, NCONSEC, NBRANCH, NPRODUCT, DEFFECTDATE) VALUES (:NVEHTYPE, :NUSE, :NOLDEST, :NCAPITALMIN, :NCURRENCY, (SELECT NVL(MAX(ISSUECONTROLS.NCONSEC) + 1, 1) FROM ISSUECONTROLS WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NVEHTYPE = :NVEHTYPE AND DEFFECTDATE <= :DEFFECTDATE AND (DNULLDATE  IS NULL OR  DNULLDATE  > :DEFFECTDATE)), :NBRANCH, :NPRODUCT, :DEFFECTDATE)"

                                    rdb.Parameters.Add("NVEHTYPE", mobjValues.StringToType(.Form.Item("NVEHTYPE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("NUSE", mobjValues.StringToType(.Form.Item("NUSE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("NOLDEST", mobjValues.StringToType(.Form.Item("NOLDEST"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 2, 0, 2, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("NCAPITALMIN", mobjValues.StringToType(.Form.Item("NCAPITALMIN"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 0, 18, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("NCURRENCY", mobjValues.StringToType(IIf(.Form.Item("NCURRENCY") = "0", "",.Form.Item("NCURRENCY")) , eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("DEFFECTDATE", mobjValues.StringToType(.QueryString.Item("DEFFECTDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)

                                    lblnPost = rdb.Run(False)

                                Case "Update"
                                    rdb = New eRemoteDB.Execute
                                    If mobjValues.StringToType(.QueryString.Item("DEFFECTDATE"), eFunctions.Values.eTypeData.etdDate) = mobjValues.StringToType(.Form.Item("dEffectdateGrid"), eFunctions.Values.eTypeData.etdDate) Then
                                        rdb.SQL = "UPDATE ISSUECONTROLS SET NOLDEST = :NOLDEST , NCAPITALMIN = :NCAPITALMIN , NCURRENCY = :NCURRENCY, NUSE = :NUSE WHERE NVEHTYPE = :NVEHTYPE AND NCONSEC = :nConsecGrid AND NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND DEFFECTDATE = :DEFFECTDATE"
                                    Else
                                        rdb.SQL = "BEGIN UPDATE ISSUECONTROLS SET DNULLDATE = :DEFFECTDATE WHERE NVEHTYPE = :NVEHTYPE AND NCONSEC = :nConsecGrid  AND NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND DEFFECTDATE = :dEffectdateGrid;"
                                        rdb.SQL = rdb.SQL & "INSERT INTO ISSUECONTROLS (NVEHTYPE,  NOLDEST, NCAPITALMIN, NCURRENCY, NCONSEC, NBRANCH, NPRODUCT, DEFFECTDATE, NUSE) VALUES (:NVEHTYPE,  :NOLDEST, :NCAPITALMIN, :NCURRENCY, (SELECT NVL(MAX(ISSUECONTROLS.NCONSEC) + 1, 1) FROM ISSUECONTROLS WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NVEHTYPE = :NVEHTYPE AND DEFFECTDATE <= :DEFFECTDATE AND (DNULLDATE  IS NULL OR  DNULLDATE  > :DEFFECTDATE)), :NBRANCH, :NPRODUCT, :DEFFECTDATE, :NUSE); END;"
                                        rdb.Parameters.Add("dEffectdateGrid", mobjValues.StringToType(.Form.Item("dEffectdateGrid"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
                                    End If
                                    rdb.Parameters.Add("NOLDEST", mobjValues.StringToType(.Form.Item("NOLDEST"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 2, 0, 2, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("NCAPITALMIN", mobjValues.StringToType(.Form.Item("NCAPITALMIN"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 0, 18, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("NCURRENCY", mobjValues.StringToType(IIf(.Form.Item("NCURRENCY") = "0", "",.Form.Item("NCURRENCY")) , eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("NUSE", mobjValues.StringToType(.Form.Item("NUSE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("NVEHTYPE", mobjValues.StringToType(.Form.Item("NVEHTYPE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)

                                    rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("DEFFECTDATE", mobjValues.StringToType(.QueryString.Item("DEFFECTDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 7, 0, 0, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("nConsecGrid", mobjValues.StringToType(.Form.Item("nConsecGrid"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)

                                    lblnPost = rdb.Run(False)
                            End Select
                        Else
                            lblnPost = True
                        End If
                    End If
            End Select
        End With

        Return lblnPost
    End Function

</script>
<%
    Response.Expires = -1441
    mobjValues = New eFunctions.Values

    mobjValues.sCodisplPage = "MAU7000val"
    mstrCommand = "sModule=Maintenance&sProject=MantAuto&sCodisplReload=" & Request.QueryString.Item("sCodispl") & "&sValPage=" & "MAU7000_val"
%>
<html>
<head>
    <title></title>
<%
    With Response
        .Write(mobjValues.StyleSheet())
        .Write(mobjValues.WindowsTitle("GE002", Request.QueryString.Item("sWindowDescript")))
    End With
%>
<script type="text/javascript" language="JavaScript" src="/VTimeNet/Scripts/GenFunctions.js"></script>
<script type="text/javascript">
//------------------------------------------------------------------------------------------
function NewLocation(Source,Codisp){
//------------------------------------------------------------------------------------------
	var lstrLocation = "";
	lstrLocation += Source.location;
	lstrLocation = lstrLocation.replace(/&OPENER=.*/,"") + "&OPENER=" + Codisp
	Source.location = lstrLocation
}
</script>
</head>
<body>
    <form id="form1" action="">
    <%
         
		    '+ Si no se han validado los campos de la página
		    If Request.Form.Item("sCodisplReload") = vbNullString Then
		        mstrErrors = insValidation()
		        Session("sErrorTable") = mstrErrors
		        Session("sForm") = Request.Form.ToString
		    Else
		        Session("sErrorTable") = vbNullString
		        Session("sForm") = vbNullString
		    End If
		
		    If mstrErrors > vbNullString Then
		        With Response
		            .Write("<script type='text/javascript' language='JavaScript'>")
		            .Write("ShowPopUp(""/VTimeNet/Common/Errors.aspx?sSource=" & Server.UrlEncode(mstrCommand) & "&sQueryString=" & Server.UrlEncode(Request.Params.Get("Query_String")) & """, ""MantPolicyError"",660,330);")
		            .Write(mobjValues.StatusControl(False, CShort(Request.QueryString.Item("nZone")), Request.QueryString.Item("WindowType")))
		            .Write("</script>")
		        End With
		    Else
		        If insPosting() Then
		            If Request.QueryString.Item("WindowType") <> "PopUp" Then
		                If Request.QueryString.Item("nAction") = CStr(eFunctions.Menues.TypeActions.clngAcceptdatafinish) Then
		                    If Request.Form.Item("sCodisplReload") = vbNullString Then
		                        Response.Write("<script type='text/javascript' language='JavaScript'>top.document.location.reload();</script>")
		                    Else
		                        Response.Write("<script type='text/javascript' language='JavaScript'>window.close();opener.insReloadTop(true,false);</script>")
		                    End If
		                Else
		                    If Request.QueryString.Item("nZone") = "1" Then
		                        If Request.Form.Item("sCodisplReload") = vbNullString Then
		                            If Request.QueryString.Item("sCodispl") = "MAU7000" Then
		                                Response.Write("<script type='text/javascript' language='JavaScript'>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_k", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sWindowDescript=" & Request.QueryString.Item("sWindowDescript") & "&nWindowTy=" & Request.QueryString.Item("nWindowTy") & mstrString & """;</script>")
		                            Else
		                                Response.Write("<script type='text/javascript' language='JavaScript'>insReloadTop();</script>")
		                            End If

		                        Else
		                            Response.Write("<script type='text/javascript' language='JavaScript'>window.close();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_k", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sWindowDescript=" & Request.QueryString.Item("sWindowDescript") & "&nWindowTy=" & Request.QueryString.Item("nWindowTy") & mstrString & """;</script>")
		                        End If

		                    Else
		                        Response.Write("<script type='text/javascript' language='JavaScript'>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_k", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sWindowDescript=" & Request.QueryString.Item("sWindowDescript") & "&nWindowTy=" & Request.QueryString.Item("nWindowTy") & """;</script>")
		                    End If
		                End If
		            Else
					
		                '+ Se recarga la página que invocó la PopUp					
		                Select Case Request.QueryString.Item("sCodispl")
		                    Case "MAU7000"
		                        Response.Write("<script type='text/javascript' language='JavaScript'>top.opener.document.location.href='MAU7000.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "' </script>")
		                End Select
		            End If		            
		        End If
		    End If
    %>
    </form>
</body>
</html>
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
                Case "MVI1014"                    
                        If CDbl(.QueryString.Item("nZone")) = 1 Then
                            Dim lclsErrors As New eFunctions.Errors

                                Dim maxEffecdate As System.DateTime
                                Dim rdb As New eRemoteDB.Execute

                            If String.IsNullOrEmpty(.Form.Item("cbeBranch")) OrElse .Form.Item("cbeBranch") = "0" Then
                                    lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 90000032)                          
                                
                                End If
                            If String.IsNullOrEmpty(.Form.Item("valProduct")) OrElse .Form.Item("valProduct") = "0" Then
                                    lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 90000033)                          
                                
                                End If
                        
                        If String.IsNullOrEmpty(.Form.Item("NCOVER")) OrElse .Form.Item("NCOVER") = "0" Then
                            lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 90000035)
                                                                
                        End If
                        If String.IsNullOrEmpty(.Form.Item("NTYPERISK")) OrElse .Form.Item("NTYPERISK") = "0" Then
                            lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 90000036)
                                                                
                        End If
                        If String.IsNullOrEmpty(.Form.Item("DEFFECDATE")) Then
                            lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 90000037)
                                
                        End If
                        rdb = New eRemoteDB.Execute
                        rdb.SQL = "SELECT  MAX(TAR_TRALIFE.DEFFECDATE) AS ROWMAX FROM INSUDB.TAR_TRALIFE TAR_TRALIFE  WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND NTYPERISK = :NTYPERISK"
                        rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.Form.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.Form.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.Form.Item("NMODULEC")), 0, .Form.Item("NMODULEC")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.Form.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                        rdb.Parameters.Add("NTYPERISK", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.Form.Item("NTYPERISK")), 7, .Form.Item("NTYPERISK")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 2, 0, 2, eRmtDataAttrib.rdbParamNullable)

       
                        If rdb.Run(True) Then
                            Do While Not rdb.EOF
                                maxEffecdate = rdb.FieldToClass("ROWMAX")
                 
                                rdb.RNext()
                            Loop
                            rdb.RCloseRec()
                        End If
                        If Request.QueryString.Item("nMainAction") <> 401 And mobjValues.StringToType(.Form.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate) < maxEffecdate Then
                            lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 10869)
                        End If

		
                        result = lclsErrors.Confirm
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            Dim lclsErrors As New eFunctions.Errors
                                                              
                            Dim rowCountKey As System.Int32
                            Dim rowCountRange0 As System.Int32
                            Dim rowCountRange1 As System.Int32
                            Dim rdb As New eRemoteDB.Execute

                            If String.IsNullOrEmpty(.Form.Item("NID")) OrElse .Form.Item("NID") = "0" Then
                                lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 90000038)
                                
                            End If
                            If String.IsNullOrEmpty(.Form.Item("NTYPE_TAR")) OrElse .Form.Item("NTYPE_TAR") = "0" Then
                                lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 90000039)
                                                                
                            End If
                            rdb = New eRemoteDB.Execute
                            rdb.SQL = "SELECT  COUNT(TAR_TRALIFE.NBRANCH) AS ROWCOUNT FROM INSUDB.TAR_TRALIFE TAR_TRALIFE  WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND DEFFECDATE = :DEFFECDATE AND NTYPERISK = :NTYPERISK AND NID = :NID"
                            rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NMODULEC")), 0, .QueryString.Item("NMODULEC")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.QueryString.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NTYPERISK", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NTYPERISK")), 7, .QueryString.Item("NTYPERISK")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 2, 0, 2, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NID", mobjValues.StringToType(.Form.Item("NID"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)

       
                            If rdb.Run(True) Then
                                Do While Not rdb.EOF
                                    rowCountKey = rdb.FieldToClass("ROWCOUNT")
                 
                                    rdb.RNext()
                                Loop
                                rdb.RCloseRec()
                            End If
                            If Request.QueryString.Item("Action") = "Add" And rowCountKey > 0 Then
                                lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 11176)
                            End If

                            rdb = New eRemoteDB.Execute
                            rdb.SQL = "SELECT  COUNT(TAR_TRALIFE.NBRANCH) AS ROWCOUNT FROM INSUDB.TAR_TRALIFE TAR_TRALIFE  WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND DEFFECDATE = :DEFFECDATE AND NTYPERISK = :NTYPERISK AND NINIPERCOV = :NINIPERCOV AND NENDPERCOV = :NENDPERCOV AND NINIPAYCOV = :NINIPAYCOV AND NENDPAYCOV = :NENDPAYCOV AND NAGE = :NAGE " 
                            rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NMODULEC")), 0, .QueryString.Item("NMODULEC")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.QueryString.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NTYPERISK", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NTYPERISK")), 7, .QueryString.Item("NTYPERISK")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 2, 0, 2, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NINIPERCOV", mobjValues.StringToType(.Form.Item("NINIPERCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NENDPERCOV", mobjValues.StringToType(.Form.Item("NENDPERCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NINIPAYCOV", mobjValues.StringToType(.Form.Item("NINIPAYCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NENDPAYCOV", mobjValues.StringToType(.Form.Item("NENDPAYCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            rdb.Parameters.Add("NAGE", mobjValues.StringToType(.Form.Item("NAGE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                            
       
                            If rdb.Run(True) Then
                                Do While Not rdb.EOF
                                    rowCountRange0 = rdb.FieldToClass("ROWCOUNT")
                 
                                    rdb.RNext()
                                Loop
                                rdb.RCloseRec()
                            End If
                            If Request.QueryString.Item("Action") = "Add" And rowCountRange0 > 0 Then
                                lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 10185)
                            End If
                            
                            If mobjValues.StringToType(.Form.Item("NPREMMEN"), eFunctions.Values.eTypeData.etdDouble) = 0 And mobjValues.StringToType(.Form.Item("NPREMWOMEN"), eFunctions.Values.eTypeData.etdDouble) = 0 And mobjValues.StringToType(.Form.Item("NPREMIUM"), eFunctions.Values.eTypeData.etdDouble) = 0 And mobjValues.StringToType(.Form.Item("NRATEMEN"), eFunctions.Values.eTypeData.etdDouble) = 0 And mobjValues.StringToType(.Form.Item("NRATEWOMEN"), eFunctions.Values.eTypeData.etdDouble) = 0 And mobjValues.StringToType(.Form.Item("NRATE"), eFunctions.Values.eTypeData.etdDouble) = 0 Then
                                lclsErrors.ErrorMessage(.QueryString.Item("sCodispl"), 55877)
                            End If
             
                                	
                            result = lclsErrors.Confirm
                        End If
                    End If
                                       
                Case Else
                    result = "insValMVI1014: Código lógico no encontrado (" & Request.QueryString.Item("sCodispl") & ")"
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
        
        Select Case Request.QueryString.Item("sCodispl")
            Case "MVI1014"
                With Request
				
                    If CDbl(.QueryString.Item("nZone")) = 1 Then
                        mstrString = "&cbeBranch=" & .Form.Item("cbeBranch")  &"&valProduct=" & .Form.Item("valProduct")  &"&NMODULEC=" & .Form.Item("NMODULEC")  &"&NCOVER=" & .Form.Item("NCOVER")  &"&NTYPERISK=" & .Form.Item("NTYPERISK")  &"&DEFFECDATE=" & .Form.Item("DEFFECDATE")                         
                        lblnPost = True	
                        			
                    Else
                        If .QueryString.Item("WindowType") = "PopUp" Then
                            mstrString = "&cbeBranch=" & .QueryString.Item("cbeBranch")  &"&valProduct=" & .QueryString.Item("valProduct")  &"&NMODULEC=" & .QueryString.Item("NMODULEC")  &"&NCOVER=" & .QueryString.Item("NCOVER")  &"&NTYPERISK=" & .QueryString.Item("NTYPERISK")  &"&DEFFECDATE=" & .QueryString.Item("DEFFECDATE")    
                        
                            Select Case .QueryString.Item("Action")
                                Case "Add" 
            rdb = New eRemoteDB.Execute 
          rdb.SQL = "INSERT INTO INSUDB.TAR_TRALIFE (NBRANCH, NPRODUCT, NMODULEC, NCOVER, DEFFECDATE, NTYPERISK, NID, NAGE, SSMOKING, NINIPERCOV, NENDPERCOV, NINIPAYCOV, NENDPAYCOV, NRATEWOMEN, NPREMWOMEN, NRATEMEN, NPREMMEN, DCOMPDATE, NUSERCODE, NTYPE_TAR, NRATE, NPREMIUM) VALUES (:NBRANCH, :NPRODUCT, :NMODULEC, :NCOVER, :DEFFECDATE, :NTYPERISK, :NID, :NAGE, :SSMOKING, :NINIPERCOV, :NENDPERCOV, :NINIPAYCOV, :NENDPAYCOV, :NRATEWOMEN, :NPREMWOMEN, :NRATEMEN, :NPREMMEN, SYSDATE, :NUSERCODE, :NTYPE_TAR, :NRATE, :NPREMIUM)"
    
 rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NMODULEC")), 0, .QueryString.Item("NMODULEC")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.QueryString.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NTYPERISK", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NTYPERISK")), 7, .QueryString.Item("NTYPERISK")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 2, 0, 2, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NID", mobjValues.StringToType(.Form.Item("NID"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NAGE", mobjValues.StringToType(.Form.Item("NAGE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                    rdb.Parameters.Add("SSMOKING", "2", eRmtDataDir.rdbParamInput, eRmtDataType.rdbChar, 1, 0, 0, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NINIPERCOV", mobjValues.StringToType(.Form.Item("NINIPERCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NENDPERCOV", mobjValues.StringToType(.Form.Item("NENDPERCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NINIPAYCOV", mobjValues.StringToType(.Form.Item("NINIPAYCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NENDPAYCOV", mobjValues.StringToType(.Form.Item("NENDPAYCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NRATEWOMEN", mobjValues.StringToType(.Form.Item("NRATEWOMEN"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 9, 6, 9, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPREMWOMEN", mobjValues.StringToType(.Form.Item("NPREMWOMEN"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 6, 18, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NRATEMEN", mobjValues.StringToType(.Form.Item("NRATEMEN"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 9, 6, 9, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPREMMEN", mobjValues.StringToType(.Form.Item("NPREMMEN"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 6, 18, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NUSERCODE", Session("NUSERCODE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NTYPE_TAR", mobjValues.StringToType(.Form.Item("NTYPE_TAR"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NRATE", mobjValues.StringToType(.Form.Item("NRATE"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 9, 6, 9, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPREMIUM", mobjValues.StringToType(.Form.Item("NPREMIUM"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 6, 18, eRmtDataAttrib.rdbParamNullable)

                                       
          lblnPost = rdb.Run(False)
                                
                                Case "Update" 
         If mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate) = mobjValues.StringToType(.Form.Item("dEffecdateCurrent"), eFunctions.Values.eTypeData.etdDate) Then 

 rdb = New eRemoteDB.Execute 
          rdb.SQL = "UPDATE INSUDB.TAR_TRALIFE SET NAGE = :NAGE , NINIPERCOV = :NINIPERCOV , NENDPERCOV = :NENDPERCOV , NINIPAYCOV = :NINIPAYCOV , NENDPAYCOV = :NENDPAYCOV , NRATEWOMEN = :NRATEWOMEN , NPREMWOMEN = :NPREMWOMEN , NRATEMEN = :NRATEMEN , NPREMMEN = :NPREMMEN, DCOMPDATE = SYSDATE, NUSERCODE = :NUSERCODE , NTYPE_TAR = :NTYPE_TAR , NRATE = :NRATE , NPREMIUM = :NPREMIUM WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND NTYPERISK = :NTYPERISK AND NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND DEFFECDATE = :DEFFECDATE AND NTYPERISK = :NTYPERISK AND NID = :NID"
    
 rdb.Parameters.Add("NAGE", mobjValues.StringToType(.Form.Item("NAGE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NINIPERCOV", mobjValues.StringToType(.Form.Item("NINIPERCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NENDPERCOV", mobjValues.StringToType(.Form.Item("NENDPERCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NINIPAYCOV", mobjValues.StringToType(.Form.Item("NINIPAYCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NENDPAYCOV", mobjValues.StringToType(.Form.Item("NENDPAYCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NRATEWOMEN", mobjValues.StringToType(.Form.Item("NRATEWOMEN"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 9, 6, 9, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPREMWOMEN", mobjValues.StringToType(.Form.Item("NPREMWOMEN"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 6, 18, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NRATEMEN", mobjValues.StringToType(.Form.Item("NRATEMEN"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 9, 6, 9, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPREMMEN", mobjValues.StringToType(.Form.Item("NPREMMEN"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 6, 18, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NUSERCODE", Session("NUSERCODE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NTYPE_TAR", mobjValues.StringToType(.Form.Item("NTYPE_TAR"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NRATE", mobjValues.StringToType(.Form.Item("NRATE"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 9, 6, 9, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPREMIUM", mobjValues.StringToType(.Form.Item("NPREMIUM"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 6, 18, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NMODULEC")), 0, .QueryString.Item("NMODULEC")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.QueryString.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NTYPERISK", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NTYPERISK")), 7, .QueryString.Item("NTYPERISK")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 2, 0, 2, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NID", mobjValues.StringToType(.Form.Item("NID"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)

                                       
          lblnPost = rdb.Run(False)
         Else 

 rdb = New eRemoteDB.Execute 
          rdb.SQL = "UPDATE INSUDB.TAR_TRALIFE SET DNULLDATE = :DNULLDATE, DCOMPDATE = SYSDATE, NUSERCODE = :NUSERCODE WHERE NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND NTYPERISK = :NTYPERISK AND NBRANCH = :NBRANCH AND NPRODUCT = :NPRODUCT AND NMODULEC = :NMODULEC AND NCOVER = :NCOVER AND DEFFECDATE = :DEFFECDATE AND NTYPERISK = :NTYPERISK AND NID = :NID"
    
 rdb.Parameters.Add("DNULLDATE", mobjValues.StringToType(.QueryString.Item("dEffecdate"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NUSERCODE", Session("NUSERCODE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NMODULEC")), 0, .QueryString.Item("NMODULEC")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.QueryString.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NTYPERISK", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NTYPERISK")), 7, .QueryString.Item("NTYPERISK")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 2, 0, 2, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.Form.Item("dEffecdateCurrent"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NID", mobjValues.StringToType(.Form.Item("NID"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)

                                       
          lblnPost = rdb.Run(False)
rdb = New eRemoteDB.Execute 
          rdb.SQL = "INSERT INTO INSUDB.TAR_TRALIFE (NBRANCH, NPRODUCT, NMODULEC, NCOVER, DEFFECDATE, NTYPERISK, NID, NAGE, SSMOKING, NINIPERCOV, NENDPERCOV, NINIPAYCOV, NENDPAYCOV, NRATEWOMEN, NPREMWOMEN, NRATEMEN, NPREMMEN, DCOMPDATE, NUSERCODE, NTYPE_TAR, NRATE, NPREMIUM) VALUES (:NBRANCH, :NPRODUCT, :NMODULEC, :NCOVER, :DEFFECDATE, :NTYPERISK, :NID, :NAGE, :SSMOKING, :NINIPERCOV, :NENDPERCOV, :NINIPAYCOV, :NENDPAYCOV, :NRATEWOMEN, :NPREMWOMEN, :NRATEMEN, :NPREMMEN, SYSDATE, :NUSERCODE, :NTYPE_TAR, :NRATE, :NPREMIUM)"
    
 rdb.Parameters.Add("NBRANCH", mobjValues.StringToType(.QueryString.Item("cbeBranch"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPRODUCT", mobjValues.StringToType(.QueryString.Item("valProduct"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NMODULEC", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NMODULEC")), 0, .QueryString.Item("NMODULEC")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NCOVER", mobjValues.StringToType(.QueryString.Item("NCOVER"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("DEFFECDATE", mobjValues.StringToType(.QueryString.Item("DEFFECDATE"), eFunctions.Values.eTypeData.etdDate), eRmtDataDir.rdbParamInput, eRmtDataType.rdbDate, 0, 0, 0, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NTYPERISK", mobjValues.StringToType(IIf(String.IsNullOrEmpty(.QueryString.Item("NTYPERISK")), 7, .QueryString.Item("NTYPERISK")), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 2, 0, 2, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NID", mobjValues.StringToType(.Form.Item("NID"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 10, 0, 10, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NAGE", mobjValues.StringToType(.Form.Item("NAGE"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
                                        rdb.Parameters.Add("SSMOKING", "2", eRmtDataDir.rdbParamInput, eRmtDataType.rdbChar, 1, 0, 0, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NINIPERCOV", mobjValues.StringToType(.Form.Item("NINIPERCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NENDPERCOV", mobjValues.StringToType(.Form.Item("NENDPERCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NINIPAYCOV", mobjValues.StringToType(.Form.Item("NINIPAYCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NENDPAYCOV", mobjValues.StringToType(.Form.Item("NENDPAYCOV"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NRATEWOMEN", mobjValues.StringToType(.Form.Item("NRATEWOMEN"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 9, 6, 9, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPREMWOMEN", mobjValues.StringToType(.Form.Item("NPREMWOMEN"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 6, 18, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NRATEMEN", mobjValues.StringToType(.Form.Item("NRATEMEN"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 9, 6, 9, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPREMMEN", mobjValues.StringToType(.Form.Item("NPREMMEN"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 6, 18, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NUSERCODE", Session("NUSERCODE"), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NTYPE_TAR", mobjValues.StringToType(.Form.Item("NTYPE_TAR"), eFunctions.Values.eTypeData.etdLong), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 5, 0, 5, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NRATE", mobjValues.StringToType(.Form.Item("NRATE"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 9, 6, 9, eRmtDataAttrib.rdbParamNullable)
rdb.Parameters.Add("NPREMIUM", mobjValues.StringToType(.Form.Item("NPREMIUM"), eFunctions.Values.eTypeData.etdDouble), eRmtDataDir.rdbParamInput, eRmtDataType.rdbNumeric, 18, 6, 18, eRmtDataAttrib.rdbParamNullable)

                                       
          lblnPost = rdb.Run(False)
        End if
                           
                            End Select
                        Else
                            lblnPost = True
                        End If
                    End If
                End With
			
        End Select
	
        Return lblnPost
    End Function

</script>
<%
    Response.Expires = -1441
    mobjValues = New eFunctions.Values

    mobjValues.sCodisplPage = "MVI1014val"
    mstrCommand = "sModule=Maintenance&sProject=MantPolicy&sCodisplReload=" & Request.QueryString.Item("sCodispl")
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
		                        Response.Write("<script type='text/javascript' language='JavaScript'>closeWinErrors();opener.insReloadTop(true,false);</script>")
		                    End If
		                Else
		                    If Request.QueryString.Item("nZone") = "1" Then
		                        If Request.Form.Item("sCodisplReload") = vbNullString Then
		                            If Request.QueryString.Item("sCodispl") = "MVI1014" Then
		                                Response.Write("<script type='text/javascript' language='JavaScript'>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_k", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sWindowDescript=" & Request.QueryString.Item("sWindowDescript") & "&nWindowTy=" & Request.QueryString.Item("nWindowTy") & mstrString & """;</script>")
		                            Else
		                                Response.Write("<script type='text/javascript' language='JavaScript'>insReloadTop();</script>")
		                            End If

		                        Else
		                            Response.Write("<script type='text/javascript' language='JavaScript'>closeWinErrors();opener.top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_k", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sWindowDescript=" & Request.QueryString.Item("sWindowDescript") & "&nWindowTy=" & Request.QueryString.Item("nWindowTy") & mstrString & """;</script>")
		                        End If

		                    Else
		                        Response.Write("<script type='text/javascript' language='JavaScript'>top.fraFolder.document.location=""" & Replace(UCase(Request.QueryString.Item("sCodispl")), "_k", "") & ".aspx?nMainAction=" & Request.QueryString.Item("nMainAction") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&sWindowDescript=" & Request.QueryString.Item("sWindowDescript") & "&nWindowTy=" & Request.QueryString.Item("nWindowTy") & """;</script>")
		                    End If
		                End If
		            Else
					
		                '+ Se recarga la página que invocó la PopUp					
		                Select Case Request.QueryString.Item("sCodispl")
		                    Case "MVI1014"
		                        Response.Write("<script type='text/javascript' language='JavaScript'>top.opener.document.location.href='MVI1014.aspx?Reload=" & Request.Form.Item("chkContinue") & "&ReloadAction=" & Request.QueryString.Item("Action") & "&ReloadIndex=" & Request.QueryString.Item("ReloadIndex") & "&sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=" & Request.QueryString.Item("nMainAction") & mstrString & "' </script>")
		                End Select
		            End If		            
		        End If
		    End If
    %>
    </form>
</body>
</html>
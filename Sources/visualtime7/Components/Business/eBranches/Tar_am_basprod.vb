Option Strict Off
Option Explicit On
Public Class Tar_am_basprod
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_am_basprod.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 20                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Estructura de tabla TAR_AM_BASPROD al 05-22-2002 12:13:50
	'+     Property                Type         DBType   Size Scale  Prec  Null
	'+-------------------------------------------------------------------------
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nTariff As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public dCompdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nBenef_type As Integer ' NUMBER     22   0     5    S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	Public sDefaulti As String ' CHAR       1    0     0    S
	Public nDed_amount As Double ' NUMBER     22   6     18   S
	Public nLimit As Double ' NUMBER     22   6     18   S
	Public sChanges As String ' CHAR       1    0     0    S
	
	Public dEffecdate_Bas As Date ' datetime  no       8                 no       (n/a)              (n/a)
	Public dEffecdate_Temp As Date ' datetime  no       8                 no       (n/a)              (n/a)
	Public nGroup As Integer ' NUMBER    no       2      5    0     yes      (n/a)              (n/a)
	Public nRole As Integer ' NUMBER    no       2      5    0     yes      (n/a)              (n/a)
	
	Public nGroup_comp As Integer ' NUMBER     22   0     5    N
	Public nAge_init As Integer ' NUMBER     22   0     5    N
	Public nAge_end As Integer ' NUMBER     22   0     5    S
	Public nPremium As Double ' NUMBER     22   2     10   S
	
	'-Variables que almacenaran los valores para condicionar la consulta
	Private mlngBranch_tmp As Integer
	Private mlngProduct_tmp As Integer
	Private mdtmEffecdate_tmp As Date

    Private mintModulec_tmp As Integer
    Private mintCover_tmp As Integer

    Public nModulec As Integer
    Public nCover As Integer
    Public sDescript As String

	'-Variable que contiene el estado del registro
	Public nStatInstanc As Tar_am_bas.eStatusInstance1
	
	'-Se declara el tipo definido al que se le asociará el arreglo que contendrá los
	'-datos traídos de la tabla
	Private Structure typTar_am_basprod
		Dim nStatInstanc As Tar_am_bas.eStatusInstance1
		Dim nBranch As Integer
		Dim nProduct As Integer
		Dim nTariff As Integer
		Dim dEffecdate As Date
		Dim dNulldate As Date
		Dim nBenef_type As Integer
		Dim sDefaulti As String
		Dim nDed_amount As Double
		Dim nLimit As Double
		Dim sChanges As String
        Dim nModulec As Integer
        Dim nCover As Integer
        Dim sDescript As String
    End Structure
	
	Private mudtTar_am_basprod() As typTar_am_basprod
	
	'-Variable utilizada para indicar si el arreglo tiene contenido o no
	Private mblnCharge As Boolean
	
	'%Load: Permite consultar las tarifas de Atención médica de un producto
    Public Function Load(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, _
                         Optional ByVal bFind As Boolean = False, Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0) As Boolean
        Dim lrecreaTar_am_basprod As eRemoteDB.Execute
        Dim lintPos As Integer

        On Error GoTo reaTar_am_basprod_Err

        If nBranch <> mlngBranch_tmp Or nProduct <> mlngProduct_tmp Or _
            dEffecdate <> mdtmEffecdate_tmp Or mintModulec_tmp <> nModulec Or _
            mintCover_tmp <> nCover Or bFind Then

            lrecreaTar_am_basprod = New eRemoteDB.Execute

            With lrecreaTar_am_basprod
                .StoredProcedure = "reaTar_am_basprod"
                .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


                If .Run Then
                    ReDim mudtTar_am_basprod(50)
                    lintPos = 0
                    Do While Not .EOF
                        mudtTar_am_basprod(lintPos).nStatInstanc = Insured_he.eStatusInstance.eftExist
                        mudtTar_am_basprod(lintPos).nBranch = nBranch
                        mudtTar_am_basprod(lintPos).nProduct = nProduct
                        mudtTar_am_basprod(lintPos).nTariff = .FieldToClass("nTariff")
                        mudtTar_am_basprod(lintPos).dEffecdate = .FieldToClass("dEffecdate")
                        mudtTar_am_basprod(lintPos).dNulldate = .FieldToClass("dNulldate")
                        mudtTar_am_basprod(lintPos).nBenef_type = .FieldToClass("nBenef_type")
                        mudtTar_am_basprod(lintPos).sDefaulti = .FieldToClass("sDefaulti")
                        mudtTar_am_basprod(lintPos).nDed_amount = .FieldToClass("nDed_amount")
                        mudtTar_am_basprod(lintPos).nLimit = .FieldToClass("nLimit")
                        mudtTar_am_basprod(lintPos).sChanges = .FieldToClass("sChanges")

                        mudtTar_am_basprod(lintPos).nModulec = nModulec
                        mudtTar_am_basprod(lintPos).nCover = nCover
                        mudtTar_am_basprod(lintPos).sDescript = sDescript

                        lintPos = lintPos + 1
                        .RNext()
                    Loop

                    Load = True

                    ReDim Preserve mudtTar_am_basprod(lintPos - 1)
                    .RCloseRec()

                    mlngBranch_tmp = nBranch
                    mlngProduct_tmp = nProduct
                    mdtmEffecdate_tmp = dEffecdate
                    mintModulec_tmp = nModulec
                    mintCover_tmp = nCover

                End If
            End With
        Else
            Load = True
        End If
        mblnCharge = Load

reaTar_am_basprod_Err:
        If Err.Number Then
            Load = False
        End If
        'UPGRADE_NOTE: Object lrecreaTar_am_basprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTar_am_basprod = Nothing
        On Error GoTo 0
    End Function
	
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Tar_am_basprod". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lreccreTar_am_basprod As eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo creTar_am_basprod_Err
		
		lreccreTar_am_basprod = New eRemoteDB.Execute
		
		With lreccreTar_am_basprod
			.StoredProcedure = "creTar_am_basprod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenef_type", nBenef_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDed_amount", nDed_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimit", nLimit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChanges", sChanges, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Add = True

                lintCount = CountItem + 1

                ReDim Preserve mudtTar_am_basprod(lintCount)

                mudtTar_am_basprod(lintCount).nStatInstanc = Insured_he.eStatusInstance.eftExist
                mudtTar_am_basprod(lintCount).nBranch = nBranch
                mudtTar_am_basprod(lintCount).nProduct = nProduct
                mudtTar_am_basprod(lintCount).nTariff = nTariff
                mudtTar_am_basprod(lintCount).dEffecdate = dEffecdate
                mudtTar_am_basprod(lintCount).dNulldate = dNulldate
                mudtTar_am_basprod(lintCount).nBenef_type = nBenef_type
                mudtTar_am_basprod(lintCount).sDefaulti = sDefaulti
                mudtTar_am_basprod(lintCount).nDed_amount = nDed_amount
                mudtTar_am_basprod(lintCount).nLimit = nLimit
                mudtTar_am_basprod(lintCount).sChanges = sChanges

                mudtTar_am_basprod(lintCount).nModulec = nModulec
                mudtTar_am_basprod(lintCount).nCover = nCover
                mudtTar_am_basprod(lintCount).sDescript = sDescript

                mblnCharge = True
            End If
		End With
		
creTar_am_basprod_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreTar_am_basprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTar_am_basprod = Nothing
		On Error GoTo 0
	End Function
	
	'%Update: Este método se encarga de actualizar registros en la tabla "Tar_am_basprod". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Update() As Boolean
		Dim lintPos As Integer
		Dim lrecupdTar_am_basprod As eRemoteDB.Execute
		
		On Error GoTo updTar_am_basprod_Err
		
		lrecupdTar_am_basprod = New eRemoteDB.Execute
		
		With lrecupdTar_am_basprod
			.StoredProcedure = "updTar_am_basprod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenef_type", nBenef_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDefaulti", sDefaulti, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nDed_amount", nDed_amount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nLimit", nLimit, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sChanges", sChanges, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run(False) Then
				Update = True
				lintPos = Position(nTariff)
				mudtTar_am_basprod(lintPos).nStatInstanc = nStatInstanc
				mudtTar_am_basprod(lintPos).dEffecdate = dEffecdate
				mudtTar_am_basprod(lintPos).dNulldate = dNulldate
				mudtTar_am_basprod(lintPos).nBenef_type = nBenef_type
				mudtTar_am_basprod(lintPos).sDefaulti = sDefaulti
				mudtTar_am_basprod(lintPos).nDed_amount = nDed_amount
				mudtTar_am_basprod(lintPos).nLimit = nLimit
				mudtTar_am_basprod(lintPos).sChanges = sChanges

                mudtTar_am_basprod(lintPos).nModulec = nModulec
                mudtTar_am_basprod(lintPos).nCover = nCover
                mudtTar_am_basprod(lintPos).sDescript = sDescript
            End If
		End With
		
updTar_am_basprod_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecupdTar_am_basprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTar_am_basprod = Nothing
		On Error GoTo 0
	End Function
	
	'%Delete: Este método se encarga de eliminar registros en la tabla "Tar_am_basprod". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		Dim lrecdelTar_am_basprod As eRemoteDB.Execute
		Dim lintPos As Integer
		
		On Error GoTo delTar_am_basprod_Err
		
		lrecdelTar_am_basprod = New eRemoteDB.Execute
		
		With lrecdelTar_am_basprod
			.StoredProcedure = "delTar_am_basprod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Delete = True
                lintPos = Position(nTariff)
                Do While lintPos < CountItem
                    mudtTar_am_basprod(lintPos).nStatInstanc = mudtTar_am_basprod(lintPos + 1).nStatInstanc
                    mudtTar_am_basprod(lintPos).nBranch = mudtTar_am_basprod(lintPos + 1).nBranch
                    mudtTar_am_basprod(lintPos).nProduct = mudtTar_am_basprod(lintPos + 1).nProduct
                    mudtTar_am_basprod(lintPos).nTariff = mudtTar_am_basprod(lintPos + 1).nTariff
                    mudtTar_am_basprod(lintPos).dEffecdate = mudtTar_am_basprod(lintPos + 1).dEffecdate
                    mudtTar_am_basprod(lintPos).dNulldate = mudtTar_am_basprod(lintPos + 1).dNulldate
                    mudtTar_am_basprod(lintPos).nBenef_type = mudtTar_am_basprod(lintPos + 1).nBenef_type
                    mudtTar_am_basprod(lintPos).sDefaulti = mudtTar_am_basprod(lintPos + 1).sDefaulti
                    mudtTar_am_basprod(lintPos).nDed_amount = mudtTar_am_basprod(lintPos + 1).nDed_amount
                    mudtTar_am_basprod(lintPos).nLimit = mudtTar_am_basprod(lintPos + 1).nLimit
                    mudtTar_am_basprod(lintPos).sChanges = mudtTar_am_basprod(lintPos + 1).sChanges

                    mudtTar_am_basprod(lintPos).nModulec = mudtTar_am_basprod(lintPos + 1).nModulec
                    mudtTar_am_basprod(lintPos).nCover = mudtTar_am_basprod(lintPos + 1).nCover

                    lintPos = lintPos + 1
                Loop
                If lintPos - 1 < 0 Then
                    ReDim Preserve mudtTar_am_basprod(0)
                    mblnCharge = False
                Else
                    ReDim Preserve mudtTar_am_basprod(lintPos - 1)
                End If
            End If
		End With
		
delTar_am_basprod_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecdelTar_am_basprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTar_am_basprod = Nothing
		On Error GoTo 0
	End Function
	
	'%Item: Permite encontrar un elemento del arreglo por su posición
	Public Function Item(ByVal nIndex As Integer) As Boolean
		If nIndex <= CountItem Then
			Item = True
			With mudtTar_am_basprod(nIndex)
				nStatInstanc = .nStatInstanc
				nBranch = .nBranch
				nProduct = .nProduct
				nTariff = .nTariff
				dEffecdate = .dEffecdate
				dNulldate = .dNulldate
				nBenef_type = .nBenef_type
				sDefaulti = .sDefaulti
				nDed_amount = .nDed_amount
				nLimit = .nLimit
                sChanges = .sChanges
                nModulec = .nModulec
                nCover = .nCover
			End With
		End If
	End Function
	
	'%FindItem: Permite encontrar un elemento del arreglo de acuerdo al código de la tarifa
	Public Function FindItem(ByVal nTariff As Integer, Optional ByVal bItem As Boolean = False) As Boolean
		Dim lintPos As Integer
		Dim lblnFind As Boolean
		
		lintPos = 0
		
		Do While lintPos <= CountItem And Not lblnFind
			If mudtTar_am_basprod(lintPos).nTariff = nTariff Then
				lblnFind = True
				FindItem = IIf(bItem, Item(lintPos), True)
			End If
			lintPos = lintPos + 1
		Loop 
	End Function
	
	'%Position: Permite devolver la posición en la que se encuentra un elemento del arreglo
	Private Function Position(ByVal nTariff As Integer) As Integer
		Dim lintPos As Integer
		Dim lblnFind As Boolean
		
		lintPos = 0
		lblnFind = False
		
		Position = -1
		
		Do While lintPos <= CountItem And Not lblnFind
			If mudtTar_am_basprod(lintPos).nTariff = nTariff Then
				lblnFind = True
				Position = lintPos
			End If
			lintPos = lintPos + 1
		Loop 
	End Function
	
	'*CountItem: Propiedad que indica el número de elementos en el arreglo
	Public ReadOnly Property CountItem() As Integer
		Get
			If mblnCharge Then
				CountItem = UBound(mudtTar_am_basprod)
			Else
				CountItem = -1
			End If
		End Get
	End Property
	
	'%Class_Initialize: Controla la creación de una instancia de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nBranch = eRemoteDB.Constants.intNull
		nProduct = eRemoteDB.Constants.intNull
		nTariff = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		dNulldate = dtmNull
		nBenef_type = eRemoteDB.Constants.intNull
		sDefaulti = strNull
		
		mlngBranch_tmp = eRemoteDB.Constants.intNull
		mlngProduct_tmp = eRemoteDB.Constants.intNull
        mdtmEffecdate_tmp = dtmNull

        nModulec = eRemoteDB.Constants.intNull
        nCover = eRemoteDB.Constants.intNull

        mintModulec_tmp = eRemoteDB.Constants.intNull
        mintCover_tmp = eRemoteDB.Constants.intNull
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Find_nTariff: Busca si existe una tarifa en la tabla TAR_AM_BASPROD
    Public Function Find_nTariff(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, _
                                 ByVal nTariff As Integer, Optional ByRef bFind As Boolean = False, _
                                 Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0) As Boolean
        Dim lrecreaTar_am_basprod_tariff As eRemoteDB.Execute

        On Error GoTo reaTar_am_basprod_tariff_Err

        lrecreaTar_am_basprod_tariff = New eRemoteDB.Execute

        With lrecreaTar_am_basprod_tariff
            .StoredProcedure = "reaTar_am_basprod_tariff"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            If .Run Then
                sDefaulti = .FieldToClass("sDefaulti")
                sChanges = .FieldToClass("sChanges")
                nBenef_type = .FieldToClass("nBenef_type")
                nLimit = .FieldToClass("nLimit")
                nDed_amount = .FieldToClass("nDed_amount")
                dEffecdate_Bas = .FieldToClass("dEffecdate")
                nModulec = .FieldToClass("nModulec")
                nCover = .FieldToClass("nCover")
                sDescript = .FieldToClass("sDescript")

                Find_nTariff = True
                .RCloseRec()
            End If
        End With

reaTar_am_basprod_tariff_Err:
        If Err.Number Then
            Find_nTariff = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecreaTar_am_basprod_tariff may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTar_am_basprod_tariff = Nothing
    End Function
	
	'%InsPostDP057: Esta función se encarga de crear/actualizar los registros
	'%correspondientes en la tabla Tar_am_deprod
    Public Function insPostDP057(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, _
                                 ByVal nProduct As Integer, ByVal nTariff As Integer, ByVal sChanges As String, _
                                 ByVal sDefaulti As String, ByVal nBenef_type As Integer, ByVal nLimit As Double, _
                                 ByVal nDed_amount As Double, ByVal nAge_init As Integer, ByVal nAge_end As Integer, _
                                 ByVal nGroup_comp As Integer, ByVal dEffecdate As Date, ByVal nPremium As Double, _
                                 ByVal nUsercode As Integer, ByVal sWindow As String, ByVal nModulec As Integer, _
                                 ByVal nCover As Integer, ByVal sDescript As String) As Boolean

        Dim lclsTar_am_detrod As eBranches.Tar_am_detprod
        Dim lblnTariff As Boolean
        Dim lblnChangeBasprod As Boolean
        Dim lblnTar_am_basprod As Boolean

        On Error GoTo insPostDP057_err

        lblnTar_am_basprod = Load(nBranch, nProduct, dEffecdate, False, nModulec, nCover)
        lblnTariff = Find_nTariff(nBranch, nProduct, dEffecdate, nTariff, False, nModulec, nCover)

        '+ Se asignan los valores de los check
        sChanges = IIf(sChanges = String.Empty, "2", sChanges)
        sDefaulti = IIf(sDefaulti = String.Empty, "2", sDefaulti)

        '+ Si no existen tarifas registradas se deja por defecto la primera que se ingresa
        If Not lblnTar_am_basprod Then
            sDefaulti = "1"
        Else
            '+ Si existen tarifas registradas se verifica si ya existe una por defecto
            If Find_sDefaulti(nBranch, nProduct, dEffecdate, nTariff, nModulec, nCover) Then
                '+ Si existe tarifa por defecto y la actual también es por defecto
                If sDefaulti = "1" Then
                    '+ Se actualiza el registro que tiene tarifa por defecto
                    Call Update_sDefaulti(nBranch, nProduct, dEffecdate, nTariff, nModulec, nCover)
                End If
            End If
        End If

        With Me
            .nBranch = nBranch
            .nProduct = nProduct
            .nTariff = nTariff
            .nAge_init = nAge_init
            .nAge_end = nAge_end
            .nGroup_comp = nGroup_comp
            .dEffecdate = dEffecdate
            .dNulldate = dNulldate
            .nPremium = nPremium
            .nUsercode = nUsercode

            .sChanges = sChanges
            .sDefaulti = sDefaulti
            .nBenef_type = nBenef_type
            .nLimit = nLimit
            .nDed_amount = nDed_amount

            .nModulec = nModulec
            .nCover = nCover
            .sDescript = sDescript
        End With

        insPostDP057 = True

        If Not lblnTariff Then
            insPostDP057 = Add()
        Else
            If dEffecdate_Bas <> dEffecdate Then
                dEffecdate_Temp = dEffecdate
                Me.dNulldate = dEffecdate
                Me.dEffecdate = dEffecdate_Bas
                insPostDP057 = Updatenulldate()
                Me.dNulldate = dtmNull
                Me.dEffecdate = dEffecdate_Temp
                insPostDP057 = Add()
                lblnChangeBasprod = True
            Else
                insPostDP057 = Update()
            End If
        End If

        lclsTar_am_detrod = New eBranches.Tar_am_detprod
        If sWindow = "PopUp" Then
            insPostDP057 = lclsTar_am_detrod.insPostDetpro(sCodispl, sAction, nBranch,
                                                           nProduct, nTariff, nAge_init,
                                                           nAge_end, nGroup_comp,
                                                           dEffecdate, nPremium, nUsercode,
                                                           nModulec, nCover)
        End If

        If lblnChangeBasprod Then
            insPostDP057 = lclsTar_am_detrod.insChangeAllDetprod(nBranch, nProduct, nTariff,
                                                                 dEffecdate_Bas, dEffecdate, nUsercode,
                                                                 nModulec, nCover)
        End If


insPostDP057_err:
        If Err.Number Then
            insPostDP057 = False
        End If
        'UPGRADE_NOTE: Object lclsTar_am_detrod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_am_detrod = Nothing
        On Error GoTo 0
    End Function
	
	'%insValDP057: Esta función se encarga de validar los datos introducidos en la zona de detalle para
	'%forma.
    Public Function insValDP057(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, _
                                ByVal nProduct As Integer, ByVal nTariff As Integer, ByVal nBenef_type As Integer, _
                                ByVal sDefaulti As String, ByVal nAge_init As Integer, ByVal nAge_end As Integer, _
                                ByVal nGroup_comp As Integer, ByVal dEffecdate As Date, ByVal nLimit As Double, _
                                ByVal nDed_amount As Double, ByVal nPremium As Double, ByVal sWindows As String, _
                                ByVal nModulec As Integer, ByVal nCover As Integer) As String
        '- Se define la variable lclsErrors para el envío de errores de la ventana
        Dim lclsErrors As eFunctions.Errors
        Dim lclsvalField As eFunctions.valField
        Dim lintExist As Integer
        Dim lclsModul As Object


        lclsErrors = New eFunctions.Errors
        lclsvalField = New eFunctions.valField

        On Error GoTo insValDP057_Err

        With lclsErrors
            'Validacion del campo de modulo
            lclsModul = New eProduct.Tab_moduls
            If lclsModul.Find(nBranch, nProduct, dEffecdate) Then
                If nModulec = eRemoteDB.Constants.intNull Then
                    Call .ErrorMessage(sCodispl, 6, , eFunctions.Errors.TextAlign.LeftAling)
                End If
            End If
            lclsModul = Nothing

            '+ Validación del campo "Tarifa".
            If nTariff = 0 Then
                Call .ErrorMessage(sCodispl, 6020, , eFunctions.Errors.TextAlign.LeftAling, "Tarifa:")
            End If

            If nTariff = eRemoteDB.Constants.intNull Then
                Call .ErrorMessage(sCodispl, 1012, , eFunctions.Errors.TextAlign.LeftAling, "Tarifa:")
            End If

            '+ Validación del campo "Tipo de Beneficio".
            If nBenef_type = eRemoteDB.Constants.intNull Or nBenef_type = 0 Then
                Call .ErrorMessage(sCodispl, 3977)
            End If

            '+ Validación del campo limite
            If nLimit = eRemoteDB.Constants.intNull Then
                nLimit = 0
            End If

            '+ Validación del campo deducible
            If nDed_amount = eRemoteDB.Constants.intNull Then
                nDed_amount = 0
            End If

            If sWindows = "PopUp" Then
                '+ Se efectúan las validaciones del campo "Edad inicial".

                If nAge_init = eRemoteDB.Constants.intNull Or nAge_init = 0 Then
                    Call .ErrorMessage(sCodispl, 3545)
                End If

                '+ Se efectúan las validaciones del campo edad final.
                If nAge_end = 0 Or nAge_end = eRemoteDB.Constants.intNull Then
                    Call .ErrorMessage(sCodispl, 3547)
                End If

                If (nAge_end < nAge_init) Then
                    Call .ErrorMessage(sCodispl, 3546)
                End If

                lintExist = 0
                lintExist = insValRangeAge(nBranch, nProduct, dEffecdate, nTariff, nAge_init, nAge_end, nGroup_comp, nModulec, nCover)
                If sAction = "Add" Then
                    If lintExist > 0 Then
                        Call .ErrorMessage(sCodispl, 11138, , eFunctions.Errors.TextAlign.LeftAling, "Edad inicial/Edad final:")
                    End If
                End If

                '+ Se efectúan las validaciones de la composición del grupo
                If nGroup_comp = eRemoteDB.Constants.intNull Or nGroup_comp = 0 Then
                    Call .ErrorMessage(sCodispl, 3549)
                End If

                ''+ Validación del campo prima
                If nPremium = eRemoteDB.Constants.intNull Or nPremium = 0 Then
                    Call .ErrorMessage(sCodispl, 60345)
                End If
            End If

            '+ Validación de tarifa por defecto
            '+ Se verifica que no sea la primera tarifa que se ingresa
            If Load(nBranch, nProduct, dEffecdate, False, nModulec, nCover) Then
                '+ Se verifica si no existe una tarifa por defecto
                If Not Find_sDefaulti(nBranch, nProduct, dEffecdate, nTariff, nModulec, nCover) Then
                    '+ Si no existe tarifa por defecto y la actual tampoco es por defecto
                    If sDefaulti = String.Empty Then
                        Call .ErrorMessage(sCodispl, 11420)
                    End If
                End If
            End If
            insValDP057 = lclsErrors.Confirm
        End With

insValDP057_Err:
        If Err.Number Then
            insValDP057 = insValDP057 & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsvalField may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsvalField = Nothing
    End Function
	
	'% insValRangeAge: Rutina que permite verificar si la edad está incluída dentro de otro rango
    Public Function insValRangeAge(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, _
                                   ByVal nTariff As Integer, ByVal nAge_init As Integer, ByVal nAge_end As Integer, _
                                   ByVal nGroup_comp As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Integer
        Dim lrecinsreaRangTariff As eRemoteDB.Execute

        On Error GoTo insRearangtariff_Err

        lrecinsreaRangTariff = New eRemoteDB.Execute

        With lrecinsreaRangTariff
            .StoredProcedure = "insrearangagetariff"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_end", nAge_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("bGroup_comp", nGroup_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                insValRangeAge = .FieldToClass("nExist")
                .RCloseRec()
            End If
        End With

insRearangtariff_Err:
        If Err.Number Then
            insValRangeAge = False
        End If
        'UPGRADE_NOTE: Object lrecinsreaRangTariff may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsreaRangTariff = Nothing
        On Error GoTo 0
    End Function
	
	'%Updatenulldate: Este método se encarga de actualizar registros en la tabla "Tar_am_basprod". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Updatenulldate() As Boolean
		Dim lintPos As Integer
		Dim lrecupdTar_am_basprod As eRemoteDB.Execute
		
		On Error GoTo updTar_am_basprodnulldate_Err
		
		lrecupdTar_am_basprod = New eRemoteDB.Execute
		
		With lrecupdTar_am_basprod
			.StoredProcedure = "updTar_am_basprodnulldate"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBenef_type", nBenef_type, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run(False) Then
				Updatenulldate = True
				lintPos = Position(nTariff)
				mudtTar_am_basprod(lintPos).nStatInstanc = nStatInstanc
				mudtTar_am_basprod(lintPos).dEffecdate = dEffecdate
				mudtTar_am_basprod(lintPos).dNulldate = dNulldate
			End If
		End With
		
updTar_am_basprodnulldate_Err: 
		If Err.Number Then
			Updatenulldate = False
		End If
		'UPGRADE_NOTE: Object lrecupdTar_am_basprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTar_am_basprod = Nothing
		On Error GoTo 0
	End Function
	
	'%Find_sDefaulti: Permite buscar si existe una tarifa preseleccionada
    Public Function Find_sDefaulti(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, _
                                ByVal nTariff As Integer, Optional ByVal bFind As Boolean = False, _
                                Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0) As Boolean

        Dim lrecreaTar_am_basprod_defaulti As eRemoteDB.Execute

        On Error GoTo reaTar_am_basprod_defaulti_Err

        lrecreaTar_am_basprod_defaulti = New eRemoteDB.Execute

        With lrecreaTar_am_basprod_defaulti
            .StoredProcedure = "reaTar_am_basprod_defaulti"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                Find_sDefaulti = True
            Else
                Find_sDefaulti = False
            End If
        End With

reaTar_am_basprod_defaulti_Err:
        If Err.Number Then
            Find_sDefaulti = False
        End If
        'UPGRADE_NOTE: Object lrecreaTar_am_basprod_defaulti may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTar_am_basprod_defaulti = Nothing
        On Error GoTo 0
    End Function

    '%Find_sDefaulti: Permite buscar si existe una tarifa preseleccionada
    Public Function Update_sDefaulti(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nTariff As Integer, _
                                     ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
        Dim lrecupdTar_am_basprod_defaulti As eRemoteDB.Execute

        On Error GoTo updTar_am_basprod_defaulti_Err

        lrecupdTar_am_basprod_defaulti = New eRemoteDB.Execute

        With lrecupdTar_am_basprod_defaulti
            .StoredProcedure = "updTar_am_basprod_defaulti"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update_sDefaulti = .Run(False)
        End With

updTar_am_basprod_defaulti_Err:
        If Err.Number Then
            Update_sDefaulti = False
        End If
        'UPGRADE_NOTE: Object lrecupdTar_am_basprod_defaulti may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdTar_am_basprod_defaulti = Nothing
        On Error GoTo 0
    End Function
End Class







Option Strict Off
Option Explicit On
Public Class Tar_am_detprod
	'%-------------------------------------------------------%'
	'% $Workfile:: Tar_am_detprod.cls                       $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'- Estructura de tabla TAR_AM_DETPROD al 05-22-2002 15:10:49
	'-     Property                Type         DBType   Size Scale  Prec  Null
	Public nBranch As Integer ' NUMBER     22   0     5    N
	Public nProduct As Integer ' NUMBER     22   0     5    N
	Public nTariff As Integer ' NUMBER     22   0     5    N
	Public nGroup_comp As Integer ' NUMBER     22   0     5    N
	Public nAge_init As Integer ' NUMBER     22   0     5    N
	Public dEffecdate As Date ' DATE       7    0     0    N
	Public nAge_end As Integer ' NUMBER     22   0     5    S
	Public dCompdate As Date ' DATE       7    0     0    N
	Public dNulldate As Date ' DATE       7    0     0    S
	Public nPremium As Double ' NUMBER     22   2     10   S
	Public nUsercode As Integer ' NUMBER     22   0     5    N
	
	Public dEffecdate_reg As Date 'datetime no       8                 no       (n/a)              (n/a)
	Public dEffecdate_Temp As Date 'datetime no       8                 no       (n/a)              (n/a)
	
    Public nModulec As Integer
    Public nCover As Integer


	'- Variables que almacenaran los valores para condicionar la consulta
	
	Private mintBranch_tmp As Integer
	Private mintProduct_tmp As Integer
	Private mintTariff_tmp As Integer
    Private mdtmEffecdate_tmp As Date
    Private mintModulec_tmp As Integer
    Private mintCover_tmp As Integer


	
	'-Variable que contiene el estado del registro
	
	Public nStatInstanc As Tar_am_bas.eStatusInstance1
	
	'-Se declara el tipo definido al que se le asociará el arreglo que contendrá los
	'-datos traídos de la tabla
	
	Private Structure typTar_am_detprod
		Dim nStatInstanc As Tar_am_bas.eStatusInstance1
		Dim nBranch As Integer
		Dim nProduct As Integer
		Dim nTariff As Integer
		Dim nGroup_comp As Integer
		Dim nAge_init As Integer
		Dim dEffecdate As Date
		Dim nAge_end As Integer
		Dim dNulldate As Date
        Dim nPremium As Double
        Dim nModulec As Integer
        Dim ncover As Integer
	End Structure
	
	Private mudtTar_am_detprod() As typTar_am_detprod
	
	'-Variable utilizada para indicar si el arreglo tiene contenido o no
	
	Private mblnCharge As Boolean
	
	'%Load: Permite consultar el detalle de una tarifa de Atención médica
    Public Function Load(ByVal lintBranch As Integer, ByVal lintProduct As Integer, ByVal lintTariff As Integer, _
                         ByVal ldtmEffecdate As Date, Optional ByVal lblnFind As Boolean = False, _
                         Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0) As Boolean
        Dim lrecreaTar_am_detprod As eRemoteDB.Execute
        Dim lintPos As Integer

        On Error GoTo reaTar_am_detprod_Err

        If (lintBranch <> mintBranch_tmp Or lintProduct <> mintProduct_tmp Or lintTariff <> mintTariff_tmp Or _
            ldtmEffecdate <> mdtmEffecdate_tmp Or mintModulec_tmp <> nModulec Or mintCover_tmp <> nCover) Or lblnFind Then

            lrecreaTar_am_detprod = New eRemoteDB.Execute

            '+Definición de parámetros para stored procedure 'insudb.reaTar_am_detprod'
            '+Información leída el 17/01/2000 15:31:06

            With lrecreaTar_am_detprod
                .StoredProcedure = "reaTar_am_detprod"
                .Parameters.Add("nBranch", lintBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nProduct", lintProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nTariff", lintTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("dEffecdate", ldtmEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                If .Run Then
                    ReDim mudtTar_am_detprod(50)
                    lintPos = 0
                    Do While Not .EOF
                        mudtTar_am_detprod(lintPos).nBranch = lintBranch
                        mudtTar_am_detprod(lintPos).nProduct = lintProduct
                        mudtTar_am_detprod(lintPos).nTariff = lintTariff
                        mudtTar_am_detprod(lintPos).dEffecdate = .FieldToClass("dEffecdate", dtmNull)
                        mudtTar_am_detprod(lintPos).nAge_init = .FieldToClass("nAge_init", eRemoteDB.Constants.intNull)
                        mudtTar_am_detprod(lintPos).nGroup_comp = .FieldToClass("nGroup_comp", eRemoteDB.Constants.intNull)
                        mudtTar_am_detprod(lintPos).nAge_end = .FieldToClass("nAge_end", eRemoteDB.Constants.intNull)
                        mudtTar_am_detprod(lintPos).dNulldate = .FieldToClass("dNulldate", dtmNull)
                        mudtTar_am_detprod(lintPos).nPremium = .FieldToClass("nPremium", eRemoteDB.Constants.intNull)

                        mudtTar_am_detprod(lintPos).nModulec = .FieldToClass("nModulec", eRemoteDB.Constants.intNull)
                        mudtTar_am_detprod(lintPos).ncover = .FieldToClass("nCover", eRemoteDB.Constants.intNull)


                        lintPos = lintPos + 1
                        .RNext()
                    Loop

                    Load = True
                    ReDim Preserve mudtTar_am_detprod(lintPos - 1)
                    .RCloseRec()

                    mintBranch_tmp = lintBranch
                    mintProduct_tmp = lintProduct
                    mintTariff_tmp = lintTariff
                    mdtmEffecdate_tmp = ldtmEffecdate
                    mintModulec_tmp = nModulec
                    mintCover_tmp = nCover
                End If
            End With
            'UPGRADE_NOTE: Object lrecreaTar_am_detprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
            lrecreaTar_am_detprod = Nothing
        Else
            Load = True
        End If
        mblnCharge = Load

reaTar_am_detprod_Err:
        If Err.Number Then
            Load = False
        End If

        On Error GoTo 0

    End Function
	
	'%ADD: Este método se encarga de agregar nuevos registros a la tabla "Tar_am_detprod". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Add() As Boolean
		Dim lreccreTar_am_detprod As eRemoteDB.Execute
		Dim lintCount As Integer
		
		On Error GoTo creTar_am_detprod_Err
		
		lreccreTar_am_detprod = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.creTar_am_detprod'
		'+Información leída el 17/01/2000 15:31:24
		
		With lreccreTar_am_detprod
			.StoredProcedure = "creTar_am_detprod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_comp", nGroup_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			If .Run(False) Then
				Add = True
				
				lintCount = CountItem + 1
				
				ReDim Preserve mudtTar_am_detprod(lintCount)
				
				mudtTar_am_detprod(lintCount).nStatInstanc = Insured_he.eStatusInstance.eftExist
				mudtTar_am_detprod(lintCount).nBranch = nBranch
				mudtTar_am_detprod(lintCount).nProduct = nProduct
				mudtTar_am_detprod(lintCount).nTariff = nTariff
				mudtTar_am_detprod(lintCount).nGroup_comp = nGroup_comp
				mudtTar_am_detprod(lintCount).nAge_init = nAge_init
				mudtTar_am_detprod(lintCount).dEffecdate = dEffecdate
				mudtTar_am_detprod(lintCount).nAge_end = nAge_end
				mudtTar_am_detprod(lintCount).dNulldate = dNulldate
                mudtTar_am_detprod(lintCount).nPremium = nPremium

                mudtTar_am_detprod(lintCount).nModulec = nModulec
                mudtTar_am_detprod(lintCount).ncover = nCover

				mblnCharge = True
			End If
		End With
		
creTar_am_detprod_Err: 
		If Err.Number Then
			Add = False
		End If
		'UPGRADE_NOTE: Object lreccreTar_am_detprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreTar_am_detprod = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Update: Este método se encarga de actualizar registros en la tabla "Tar_am_detprod". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.ica
	Public Function Update() As Boolean
		Dim lintPos As Integer
		Dim lrecupdTar_am_detprod As eRemoteDB.Execute
		
		On Error GoTo updTar_am_detprod_Err
		
		lrecupdTar_am_detprod = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updTar_am_detprod'
		'+Información leída el 17/01/2000 15:37:35
		
		With lrecupdTar_am_detprod
			.StoredProcedure = "updTar_am_detprod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_comp", nGroup_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_end", nAge_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Update = True
                'lintPos = Position(nAge_init)
                'mudtTar_am_detprod(lintPos).nBranch = nBranch
                'mudtTar_am_detprod(lintPos).nProduct = nProduct
                'mudtTar_am_detprod(lintPos).nTariff = nTariff
                'mudtTar_am_detprod(lintPos).nGroup_comp = nGroup_comp
                'mudtTar_am_detprod(lintPos).nAge_init = nAge_init
                'mudtTar_am_detprod(lintPos).dEffecdate = dEffecdate
                'mudtTar_am_detprod(lintPos).nAge_end = nAge_end
                'mudtTar_am_detprod(lintPos).dNulldate = dNulldate
                'mudtTar_am_detprod(lintPos).nPremium = nPremium

                'mudtTar_am_detprod(lintPos).nModulec = nModulec
                'mudtTar_am_detprod(lintPos).ncover = nCover

            End If
		End With
		
updTar_am_detprod_Err: 
		If Err.Number Then
			Update = False
		End If
		'UPGRADE_NOTE: Object lrecupdTar_am_detprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTar_am_detprod = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Delete: Este método se encarga de eliminar registros en la tabla "Tar_am_detprod". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Delete() As Boolean
		Dim lrecdelTar_am_detprod As eRemoteDB.Execute
		Dim lintPos As Integer
		
		On Error GoTo delTar_am_detprod_Err
		
		lrecdelTar_am_detprod = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.delTar_am_detprod'
		'+Información leída el 17/01/2000 15:31:57
		
		With lrecdelTar_am_detprod
			.StoredProcedure = "delTar_am_detprod"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_comp", nGroup_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                Delete = True
                'lintPos = Position(nAge_init)
                'Do While lintPos < CountItem And lintPos <> -1
                'mudtTar_am_detprod(lintPos).nStatInstanc = mudtTar_am_detprod(lintPos + 1).nStatInstanc
                'mudtTar_am_detprod(lintPos).nBranch = mudtTar_am_detprod(lintPos + 1).nBranch
                'mudtTar_am_detprod(lintPos).nProduct = mudtTar_am_detprod(lintPos + 1).nProduct
                'mudtTar_am_detprod(lintPos).nTariff = mudtTar_am_detprod(lintPos + 1).nTariff
                'mudtTar_am_detprod(lintPos).dEffecdate = mudtTar_am_detprod(lintPos + 1).dEffecdate
                'mudtTar_am_detprod(lintPos).nAge_init = mudtTar_am_detprod(lintPos + 1).nAge_init
                'mudtTar_am_detprod(lintPos).nGroup_comp = mudtTar_am_detprod(lintPos + 1).nGroup_comp
                'mudtTar_am_detprod(lintPos).nAge_end = mudtTar_am_detprod(lintPos + 1).nAge_end
                'mudtTar_am_detprod(lintPos).dNulldate = mudtTar_am_detprod(lintPos + 1).dNulldate
                'mudtTar_am_detprod(lintPos).nPremium = mudtTar_am_detprod(lintPos + 1).nPremium

                'mudtTar_am_detprod(lintPos).nModulec = nModulec
                'mudtTar_am_detprod(lintPos).ncover = nCover
                'lintPos = lintPos + 1
                'Loop
                'If lintPos - 1 < 0 Then
                'ReDim Preserve mudtTar_am_detprod(0)
                'mblnCharge = False
                'Else
                'ReDim Preserve mudtTar_am_detprod(lintPos - 1)
                'End If
            End If
        End With
		
delTar_am_detprod_Err: 
		If Err.Number Then
			Delete = False
		End If
		'UPGRADE_NOTE: Object lrecdelTar_am_detprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelTar_am_detprod = Nothing
		On Error GoTo 0
		
	End Function
	
	'%Item: Permite encontrar un elemento del arreglo por su posición
	Public Function Item(ByRef lintindex As Integer) As Boolean
		If lintindex <= CountItem Then
			Item = True
			With mudtTar_am_detprod(lintindex)
				nStatInstanc = .nStatInstanc
				nTariff = .nTariff
				nAge_init = .nAge_init
				nGroup_comp = .nGroup_comp
				nAge_end = .nAge_end
				dNulldate = .dNulldate
                nPremium = .nPremium
                nModulec = .nModulec
                nCover = .ncover
			End With
		End If
	End Function
	
	'%Position: Permite devolver la posición en la que se encuentra un elemento del arreglo
	Private Function Position(ByRef lintAge_init As Integer) As Integer
		Dim lintPos As Integer
		Dim lblnFind As Boolean
		
		lintPos = 0
		lblnFind = False
		
		Position = -1
		
		Do While lintPos <= CountItem And Not lblnFind
			If mudtTar_am_detprod(lintPos).nAge_init = lintAge_init Then
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
				CountItem = UBound(mudtTar_am_detprod)
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
		nGroup_comp = eRemoteDB.Constants.intNull
		nAge_init = eRemoteDB.Constants.intNull
		dEffecdate = dtmNull
		nAge_end = eRemoteDB.Constants.intNull
		dNulldate = dtmNull
		nPremium = eRemoteDB.Constants.intNull
        nModulec = eRemoteDB.Constants.intNull
        nCover = eRemoteDB.Constants.intNull
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%InsPostDetpro: Esta función se encarga de crear/actualizar los registros
	'%correspondientes en la tabla Tar_am_deprod
    Public Function insPostDetpro(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Integer, _
                                  ByVal nProduct As Integer, ByVal nTariff As Integer, ByVal nAge_init As Integer, _
                                  ByVal nAge_end As Integer, ByVal nGroup_comp As Integer, ByVal dEffecdate As Date, _
                                  ByVal nPremium As Double, ByVal nUsercode As Integer, ByVal nModulec As Integer, _
                                  ByVal nCover As Integer) As Boolean

        Dim lclsTar_am_basprod As eBranches.Tar_am_basprod

        Dim blnRangetariff As Object

        lclsTar_am_basprod = New eBranches.Tar_am_basprod

        On Error GoTo insPostDetpro_err

        insPostDetpro = True
        Call Load(nBranch, nProduct, nTariff, dEffecdate, nModulec, nCover)
        blnRangetariff = FindRangeAge(nBranch, nProduct, dEffecdate, nTariff, nAge_init, nAge_end, nGroup_comp, nModulec, nCover)


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
            .nModulec = nModulec
            .nCover = nCover
        End With


        Select Case sAction

            '+Si la opción seleccionada es Registrar

            Case "Add"
                If blnRangetariff Then
                    If dEffecdate_reg <> dEffecdate Then
                        dEffecdate_Temp = dEffecdate
                        Me.dNulldate = dEffecdate
                        Me.dEffecdate = dEffecdate_reg
                        insPostDetpro = Updatenulldate()
                        Me.dNulldate = dtmNull
                        Me.dEffecdate = dEffecdate_Temp
                        insPostDetpro = Add()
                    End If
                Else
                    Me.dNulldate = dtmNull
                    insPostDetpro = Add()
                End If

                '+Si la opción seleccionada es Modificar

            Case "Update"
                If dEffecdate_reg <> dEffecdate Then
                    dEffecdate_Temp = dEffecdate
                    Me.dNulldate = dEffecdate
                    Me.dEffecdate = dEffecdate_reg
                    insPostDetpro = Updatenulldate()
                    Me.dNulldate = dtmNull
                    Me.dEffecdate = dEffecdate_Temp
                    insPostDetpro = Add()
                Else
                    insPostDetpro = Update()
                End If

                '+Si la opción seleccionada es Eliminar

            Case "Del"
                If dEffecdate_reg <> dEffecdate Then
                    dEffecdate_Temp = dEffecdate
                    Me.dNulldate = dEffecdate
                    Me.dEffecdate = dEffecdate_reg
                    insPostDetpro = Updatenulldate()
                    Me.dNulldate = dtmNull
                    Me.dEffecdate = dEffecdate_Temp
                Else
                    insPostDetpro = Delete()
                End If

        End Select

        'UPGRADE_NOTE: Object lclsTar_am_basprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsTar_am_basprod = Nothing

insPostDetpro_err:
        If Err.Number Then
            insPostDetpro = False
        End If
        On Error GoTo 0

    End Function
	
	'% FindRangeAge: Rutina que permite verificar si la edad está incluída dentro de otro rango
    Public Function FindRangeAge(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, _
                                 ByVal nTariff As Integer, ByVal nAge_init As Integer, ByVal nAge_end As Integer, _
                                 ByVal nGroup_comp As Integer, ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean

        '+ Valida contra la clase

        Dim lrecinsreaRangTariff As eRemoteDB.Execute

        On Error GoTo insRearangtariff_Err

        lrecinsreaRangTariff = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.insreaRangTariff'
        '+ Información leída el 28/12/2001 01:36:46 p.m.

        With lrecinsreaRangTariff
            .StoredProcedure = "insreaRangTariff"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_end", nAge_end, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup_comp", nGroup_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                FindRangeAge = True
                Me.dEffecdate_reg = .FieldToClass("dEffecdate")
                .RCloseRec()
            End If
        End With

insRearangtariff_Err:
        If Err.Number Then
            FindRangeAge = False
        End If
        'UPGRADE_NOTE: Object lrecinsreaRangTariff may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsreaRangTariff = Nothing
        On Error GoTo 0

    End Function
	
	'%insChangeAllDetprod: Esta función se encarga de crear/actualizar los registros
	'%correspondientes en la tabla Tar_am_deprod
    Public Function insChangeAllDetprod(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTariff As Integer, _
                                        ByVal dEffecdate_reg As Date, ByVal dEffecdate As Date, ByVal nUsercode As Integer, _
                                        ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
        Dim lintPos As Integer
        Dim dEffecdate_Temp As Date

        If Load(nBranch, nProduct, nTariff, dEffecdate, nModulec, nCover) Then
            Do While Item(lintPos)
                If mudtTar_am_detprod(lintPos).dEffecdate <> dEffecdate Then
                    With Me
                        .nBranch = nBranch
                        .nProduct = nProduct
                        .nUsercode = nUsercode
                        .nTariff = nTariff
                        .nGroup_comp = mudtTar_am_detprod(lintPos).nGroup_comp
                        .nAge_init = mudtTar_am_detprod(lintPos).nAge_init
                        .nAge_end = mudtTar_am_detprod(lintPos).nAge_end
                        .nPremium = mudtTar_am_detprod(lintPos).nPremium
                        dEffecdate_Temp = dEffecdate
                        .dNulldate = dEffecdate
                        .dEffecdate = mudtTar_am_detprod(lintPos).dEffecdate
                        insChangeAllDetprod = Updatenulldate()
                        .dNulldate = dtmNull
                        .dEffecdate = dEffecdate

                        .nModulec = nModulec
                        .nCover = nCover

                        insChangeAllDetprod = Add()
                    End With
                End If

                lintPos = lintPos + 1
            Loop
        End If


    End Function
	
	'%UpdateNulldate: Este método se encarga de actualizar registros en la tabla "Tar_am_detprod". Devolviendo verdadero o
	'%falso dependiendo de si el Stored procedure se ejecutó correctamente.ica
	Public Function Updatenulldate() As Boolean
		Dim lintPos As Integer
		Dim lrecupdTar_am_detprod As eRemoteDB.Execute
		
		On Error GoTo updTar_am_detprodnulldate_Err
		
		lrecupdTar_am_detprod = New eRemoteDB.Execute
		
		'+Definición de parámetros para stored procedure 'insudb.updTar_am_detprod'
		'+Información leída el 17/01/2000 15:37:35
		
		With lrecupdTar_am_detprod
			.StoredProcedure = "updTar_am_detprodNulldate"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup_comp", nGroup_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAge_init", nAge_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            If .Run(False) Then
                Updatenulldate = True
                mudtTar_am_detprod(lintPos).nBranch = nBranch
                mudtTar_am_detprod(lintPos).nProduct = nProduct
                mudtTar_am_detprod(lintPos).nTariff = nTariff
                mudtTar_am_detprod(lintPos).nGroup_comp = nGroup_comp
                mudtTar_am_detprod(lintPos).nAge_init = nAge_init
                mudtTar_am_detprod(lintPos).dEffecdate = dEffecdate
                mudtTar_am_detprod(lintPos).dNulldate = dNulldate

                mudtTar_am_detprod(lintPos).nModulec = nModulec
                mudtTar_am_detprod(lintPos).ncover = nCover
            End If
		End With
		
updTar_am_detprodnulldate_Err: 
		If Err.Number Then
			Updatenulldate = False
		End If
		'UPGRADE_NOTE: Object lrecupdTar_am_detprod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdTar_am_detprod = Nothing
		On Error GoTo 0
		
	End Function
	
	'%valTar_am_detProd: devuelve la fecha de última modificación de la tabla
    Public Function valTar_am_detProd(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nTariff As Integer, ByVal dEffecdate As Date, _
                                      ByVal nModulec As Integer, ByVal nCover As Integer) As Boolean
        Dim lclsExecute As eRemoteDB.Execute
        Dim lintExists As Integer

        On Error GoTo valTar_am_detProd_Err

        lclsExecute = New eRemoteDB.Execute

        With lclsExecute
            .StoredProcedure = "valExistsTar_am_detProd"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTariff", nTariff, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", lintExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Run(False)
            If .Parameters("nExists").Value = 1 Then
                valTar_am_detProd = True
            End If
        End With

valTar_am_detProd_Err:
        If Err.Number Then
            valTar_am_detProd = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsExecute may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsExecute = Nothing
    End Function
End Class







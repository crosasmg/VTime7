Option Strict Off
Option Explicit On
Public Class HomeOwner
	'**+Objective: Class that supports the table Execute it's content is:
	'**+Version: $$Revision: 2 $
	'+Objetivo: Clase que le da soporte a la tabla Execute cuyo contenido es:
	'+Version: $$Revision: 2 $
	
	'**+Objective: Properties according to the table 'HomeOwner' in the system 30/06/2004 03:43:06 p.m.
	'+Objetivo: Propiedades según la tabla 'HomeOwner' en el sistema 30/06/2004 03:43:06 p.m.
	Public sCertype As String
	Public nBranch As Short
	Public nProduct As Short
	Public nPolicy As Integer
	Public nCertif As Integer
	Public nDwellingType As Short
	Public nOwnerShip As Short
	Public nYear_built As Short
	Public sCov_purc As String
	Public nPrice_purch As Double
	Public nCurrency_purch As Short
	Public dDate_purch As Date
	Public sPolicy_other As String
	Public nCap_other As Double
	Public nCurrency_other As Short
	Public dExpir_other As Date
	Public nExterConstr As Short
	Public sOther_constr As String
	Public nStories As Short
	Public nRoofType As Short
	Public nRoofYear As Short
	Public nHomeSuper As Short
	Public nLandSuper As Short
	Public nGarage As Short
	Public nFirePlace As Short
	Public nBedrooms As Short
	Public nFullBath As Short
	Public nHalfBath As Short
	Public nAirType As Short
	Public nAlt_heating As Short
	Public sGas As String
	Public sSprinkSys As String
	Public sAlarm_comp As String
	Public nDist_Hydr As Short
	Public sNon_smok As String
	Public nDist_fire As Short
	Public sFireDepart As String
    Public nFloodZone As Short
    Public nSeismicZone As Integer
	Public sFloodInd As String
	Public nSwimPool As Short
	Public sFencePool As String
	Public nFenceHeight As Short
	Public sTrampoline As String
	Public sAnimalsInd As String
	Public sAnimalsDes As String
	Public sAttackedInd As String
	Public nFoundType As Short
	
	
	
	'**%Objective: Updates a registry to the table "HomeOwner" using the key for this table.
	'**%Parameters:
	'**%     nusercode        -  código del usuario
	'**%     scertype         -  type of registry
	'**%     nbranch          -  branch
	'**%     nproduct         -  product
	'**%     npolicy          -  i number of poliza
	'**%     ncertif          -  i number of certificate
	'**%     deffecdate       -  date of effect of the registry
	'**%     ndwellingtype    -  code of type of dwelling for homeowner policies
	'**%     nownership       -  code of ownership/occupation of a home
	'**%     nyear_built      -  year of construction
	'**%     scov_purc        -  it indicates the home purchase is covered
	'**%     nprice_purch     -  purchase price of the home
	'**%     ncurrency_purch  -  code of the currency of the purchase price
	'**%     ddate_purch      -  date of purchase.
	'**%     spolicy_other    -  it indicated that the risk has been covered by  another policy
	'**%     ncap_other       -  sum insured amount of the another policy
	'**%     ncurrency_other  -  code of the currency.
	'**%     dexpir_other     -  expiry date of the another policy.
	'**%     nexterconstr     -
	'**%     sother_constr    -  description of others materials of construcción.
	'**%     nstories         -  stories of the construcción
	'**%     nrooftype        -  type of roof
	'**%     nroofyear        -  year when the roof was installed or when it was last changed
	'**%     nhomesuper       -  area (mtrs2) of the home
	'**%     nlandsuper       -  area (mts2) of the land
	'**%     ngarage          -  quantity of cars in the garage
	'**%     nfireplace       -  quantity of fire places
	'**%     nbedrooms        -  quantity of  bedroom
	'**%     nfullbath        -  quantity of full bathrooms
	'**%     nhalfbath        -  quantity of half bathrooms
	'**%     nairtype         -  type of air conditioning
	'**%     nalt_heating     -  code of  alternate heating
	'**%     sgas             -  indicator to have gas tank
	'**%     ssprinksys       -  indicator to have sprinkler system
	'**%     salarm_comp      -  name of the company that monitors the alarm
	'**%     ndist_hydr       -  distance to the nearest fire hydrant
	'**%     snon_smok        -  it indicate that is allow to smoke in the dwelling
	'**%     ndist_fire       -  distance to nearest fire department
	'**%     sfiredepart      -  name of the nearest  fire department
	'**%     nfloodzone       -  flood zone type
	'**%     sfloodind        -  indicator of flood insurance
	'**%     nswimpool        -  code of  the  swimmig pool ubication
	'**%     sfencepool       -  it indicates that the swimming pool is enclosed  by a fence
	'**%     nfenceheight     -  height of the fence (in metros)
	'**%     strampoline      -  it indicates that swimming pool with trampoline
	'**%     sanimalsind      -  it indicates that the home has pets or livestock
	'**%     sanimalsdes      -  description of the animals.
	'**%     sattackedind     -  indicator,have any of these pets attacked anyone?
	'**%     nfoundtype       -  code of foundation
	'%Objetivo: Actualiza un registro a la tabla "HomeOwner" usando la clave para dicha tabla.
	'%Parámetros:
	'%     nusercode       -   código del usuario
	'%     scertype        -   tipo de registro
	'%     nbranch         -   ramo
	'%     nproduct        -   producto
	'%     npolicy         -   numero de poliza
	'%     ncertif         -   numero de certificado
	'%     deffecdate      -   fecha de efecto del registro
	'%     ndwellingtype   -   código de tipo de vivienda para pólizas de hogar
	'%     nownership      -   código de ocupación de una vivienda
	'%     nyear_built     -   año de construcción de la vivienda
	'%     scov_purc       -   indica que se cubre el riesgo por adquisición  de casa
	'%     nprice_purch    -   precio de compra de la vivienda.
	'%     ncurrency_purch -   código de la moneda en la que está expresado el precio de compra.
	'%     ddate_purch     -   fecha de compra de la vivienda.
	'%     spolicy_other   -   indicador de que posee otra póliza cubriendo  el riesgo
	'%     ncap_other      -   monto de capital asegurado por la otra póliza
	'%     ncurrency_other -   código de la moneda en la que está expresado el monto asegurado de la otra póliza
	'%     dexpir_other    -   fecha de vencimiento de la otra póliza.
	'%     nexterconstr    -   material con el que se hizo la construcción exter
	'%     sother_constr   -   descripción de otros materiales de construcción
	'%     nstories        -   cantidad de niveles (pisos) que posee la construcción
	'%     nrooftype       -   tipo de techo.
	'%     nroofyear       -   año en que se realizó la  instalación o último  cambio al techo.
	'%     nhomesuper      -   superficie (expresada en mts2) de la vivienda.
	'%     nlandsuper      -   superficie (expresada en mts2) del terreno.
	'%     ngarage         -   cantidad de vehículos que pueden ocupar el estacio.
	'%     nfireplace      -   cantidad de chimeneas que se tienen en la vivienda
	'%     nbedrooms       -   cantidad de habitaciones que tiene la vivienda
	'%     nfullbath       -   cantidad de baños completos tiene la vivienda
	'%     nhalfbath       -   cantidad de medios baños que tiene la vivienda
	'%     nairtype        -   tipos de aire acondicionados
	'%     nalt_heating    -   código de sistema de calefacción.
	'%     sgas            -   indicador de poseer depósito de gasolina
	'%     ssprinksys      -   indicador de poseer sistema de riego
	'%     salarm_comp     -   nombre de la compañía que está a cargo del sistema  de alarma de la vivienda.
	'%     ndist_hydr      -   distancia al hidrante más cercano
	'%     snon_smok       -   indica que está permitido fumar en la vivienda
	'%     ndist_fire      -   distancia (expresada en km.) a la estación de bomberos más cerca.
	'%     sfiredepart     -   nombre de la estación de bomberos más cercana
	'%     nfloodzone      -   código de tipo de zonas de inundación
	'%     sfloodind       -   indicador de poseer o desear tener un seguro de  inundación
	'%     nswimpool       -   código de la ubicación de la piscina
	'%     sfencepool      -   indicador de piscina con cerca.
	'%     nfenceheight    -   altura de la cerca que posee la piscina
	'%     strampoline     -   indicador de pisina con trampolín
	'%     sanimalsind     -   indicador de poseer animales domésticos o ganado
	'%     sanimalsdes     -   descripción de los animales (cantidad, tipo y raza)
	'%     sattackedind    -   indicador.¿alguno de los animales ha atacado a alguien?
	'%     nfoundtype      -   código de fundaciones/cimientos/bases de la construcción.
    Private Function Update(ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nDwellingType As Short, ByVal nOwnerShip As Short, ByVal nYear_built As Short, ByVal sCov_purc As String, ByVal nPrice_purch As Double, ByVal nCurrency_purch As Short, ByVal dDate_purch As Date, ByVal sPolicy_other As String, ByVal nCap_other As Double, ByVal nCurrency_other As Short, ByVal dExpir_other As Date, ByVal nExterConstr As Short, ByVal sOther_constr As String, ByVal nStories As Short, ByVal nRoofType As Short, ByVal nRoofYear As Short, ByVal nHomeSuper As Short, ByVal nLandSuper As Short, ByVal nGarage As Short, ByVal nFirePlace As Short, ByVal nBedrooms As Short, ByVal nFullBath As Short, ByVal nHalfBath As Short, ByVal nAirType As Short, ByVal nAlt_heating As Short, ByVal sGas As String, ByVal sSprinkSys As String, ByVal sAlarm_comp As String, ByVal nDistdr As Short, ByVal sNon_smok As String, ByVal nDist_fire As Short, ByVal sFireDepart As String, ByVal nFloodZone As Short, ByVal nSeismicZone As Integer, ByVal sFloodInd As String, ByVal nSwimPool As Short, ByVal sFencePool As String, ByVal nFenceHeight As Short, ByVal sTrampoline As String, ByVal sAnimalsInd As String, ByVal sAnimalsDes As String, ByVal sAttackedInd As String, ByVal nFoundType As Short) As Boolean
        Dim lclsHomeOwner As eRemoteDB.Execute


        lclsHomeOwner = New eRemoteDB.Execute

        '+ Define all parameters for the stored procedures 'insudb.insupdHomeOwner'. Generated on 30/06/2004 03:43:06 p.m.
        With lclsHomeOwner
            .StoredProcedure = "insupdHomeOwner"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDwellingType", nDwellingType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOwnerShip", nOwnerShip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nYear_built", nYear_built, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCov_purc", sCov_purc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPrice_purch", nPrice_purch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency_purch", nCurrency_purch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDate_purch", dDate_purch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolicy_other", sPolicy_other, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCap_other", nCap_other, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency_other", nCurrency_other, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpir_other", dExpir_other, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExterConstr", nExterConstr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sOther_constr", sOther_constr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStories", nStories, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRoofType", nRoofType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRoofYear", nRoofYear, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nHomeSuper", nHomeSuper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nLandSuper", nLandSuper, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGarage", nGarage, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFirePlace", nFirePlace, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBedrooms", nBedrooms, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFullBath", nFullBath, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nHalfBath", nHalfBath, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAirType", nAirType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAlt_heating", nAlt_heating, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sGas", sGas, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSprinkSys", sSprinkSys, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAlarm_comp", sAlarm_comp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDist_Hydr", nDist_Hydr, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sNon_smok", sNon_smok, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDist_fire", nDist_fire, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFireDepart", sFireDepart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFloodZone", nFloodZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSeismicZone", nSeismicZone, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFloodInd", sFloodInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSwimPool", nSwimPool, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sFencePool", sFencePool, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFenceHeight", nFenceHeight, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTrampoline", sTrampoline, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAnimalsInd", sAnimalsInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAnimalsDes", sAnimalsDes, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAttackedInd", sAttackedInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFoundType", nFoundType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Update = .Run(False)
        End With

        lclsHomeOwner = Nothing

        Exit Function
    End Function
	
	'**%Objective: It verifies the existence of a registry in table "HomeOwner" using the key of this table.
	'**%Parameters:
	'**%     scertype   -  type of registry
	'**%     nbranch    -  branch
	'**%     nproduct   -  product
	'**%     npolicy    -  i number of poliza
	'**%     ncertif    -  i number of certificate
	'**%     deffecdate -  date of effect of the registry
	'%Objetivo: Verifica la existencia de un registro en la tabla "HomeOwner" usando la clave de dicha tabla.
	'%Parámetros:
	'%     scertype   -   tipo de registro
	'%     nbranch    -   ramo
	'%     nproduct   -   producto
	'%     npolicy    -   numero de poliza
	'%     ncertif    -   numero de certificado
	'%     deffecdate -   fecha de efecto del registro
	Private Function IsExist(ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date) As Boolean
		Dim lclsHomeOwner As eRemoteDB.Execute
		Dim lintExist As Short
		

        lclsHomeOwner = New eRemoteDB.Execute
		lintExist = 0
		
		'+ Define all parameters for the stored procedures 'insudb.valHomeOwnerExist'. Generated on 30/06/2004 03:43:06 p.m.
		With lclsHomeOwner
			.StoredProcedure = "reaHomeOwner_v"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Parameters.Add("nExist", lintExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				IsExist = (.Parameters("nExist").Value = 1)
			Else
				IsExist = False
			End If
		End With
		
		lclsHomeOwner = Nothing
		
		Exit Function
	End Function
	
	'**%Objective: Validation of the data for the page details.
	'**%Parameters:
	'**%     sCodispl         -  Logical code that identifies the transaction.
	'**%     nMainAction      -  Action being executed on the transaction.
	'**%     sAction          -  Action begin executed on the grid of the transaction
	'**%     nusercode        -  código del usuario
	'**%     scertype         -  type of registry
	'**%     nbranch          -  branch
	'**%     nproduct         -  product
	'**%     npolicy          -  i number of poliza
	'**%     ncertif          -  i number of certificate
	'**%     deffecdate       -  date of effect of the registry
	'**%     ndwellingtype    -  code of type of dwelling for homeowner policies
	'**%     nownership       -  code of ownership/occupation of a home
	'**%     nyear_built      -  year of construction
	'**%     scov_purc        -  it indicates the home purchase is covered
	'**%     nprice_purch     -  purchase price of the home
	'**%     ncurrency_purch  -  code of the currency of the purchase price
	'**%     ddate_purch      -  date of purchase.
	'**%     spolicy_other    -  it indicated that the risk has been covered by  another policy
	'**%     ncap_other       -  sum insured amount of the another policy
	'**%     ncurrency_other  -  code of the currency.
	'**%     dexpir_other     -  expiry date of the another policy.
	'**%     nexterconstr     -
	'**%     sother_constr    -  description of others materials of construcción.
	'**%     nstories         -  stories of the construcción
	'**%     nrooftype        -  type of roof
	'**%     nroofyear        -  year when the roof was installed or when it was last changed
	'**%     nhomesuper       -  area (mtrs2) of the home
	'**%     nlandsuper       -  area (mts2) of the land
	'**%     ngarage          -  quantity of cars in the garage
	'**%     nfireplace       -  quantity of fire places
	'**%     nbedrooms        -  quantity of  bedroom
	'**%     nfullbath        -  quantity of full bathrooms
	'**%     nhalfbath        -  quantity of half bathrooms
	'**%     nairtype         -  type of air conditioning
	'**%     nalt_heating     -  code of  alternate heating
	'**%     sgas             -  indicator to have gas tank
	'**%     ssprinksys       -  indicator to have sprinkler system
	'**%     salarm_comp      -  name of the company that monitors the alarm
	'**%     ndist_hydr       -  distance to the nearest fire hydrant
	'**%     snon_smok        -  it indicate that is allow to smoke in the dwelling
	'**%     ndist_fire       -  distance to nearest fire department
	'**%     sfiredepart      -  name of the nearest  fire department
	'**%     nfloodzone       -  flood zone type
	'**%     sfloodind        -  indicator of flood insurance
	'**%     nswimpool        -  code of  the  swimmig pool ubication
	'**%     sfencepool       -  it indicates that the swimming pool is enclosed  by a fence
	'**%     nfenceheight     -  height of the fence (in metros)
	'**%     strampoline      -  it indicates that swimming pool with trampoline
	'**%     sanimalsind      -  it indicates that the home has pets or livestock
	'**%     sanimalsdes      -  description of the animals.
	'**%     sattackedind     -  indicator,have any of these pets attacked anyone?
	'**%     nfoundtype       -  code of foundation
	'%Objetivo: Validación de los datos para la página detalle.
	'%Parámetros:
	'%     sCodispl        -   Código lógico que identifica la transacción.
	'%     nMainAction     -   Acción que se ejecuta sobre la transacción.
	'%     sAction         -   Acción que se ejecuta sobre el grid de la transacción.
	'%     nusercode       -   código del usuario
	'%     scertype        -   tipo de registro
	'%     nbranch         -   ramo
	'%     nproduct        -   producto
	'%     npolicy         -   numero de poliza
	'%     ncertif         -   numero de certificado
	'%     deffecdate      -   fecha de efecto del registro
	'%     ndwellingtype   -   código de tipo de vivienda para pólizas de hogar
	'%     nownership      -   código de ocupación de una vivienda
	'%     nyear_built     -   año de construcción de la vivienda
	'%     scov_purc       -   indica que se cubre el riesgo por adquisición  de casa
	'%     nprice_purch    -   precio de compra de la vivienda.
	'%     ncurrency_purch -   código de la moneda en la que está expresado el precio de compra.
	'%     ddate_purch     -   fecha de compra de la vivienda.
	'%     spolicy_other   -   indicador de que posee otra póliza cubriendo  el riesgo
	'%     ncap_other      -   monto de capital asegurado por la otra póliza
	'%     ncurrency_other -   código de la moneda en la que está expresado el monto asegurado de la otra póliza
	'%     dexpir_other    -   fecha de vencimiento de la otra póliza.
	'%     nexterconstr    -   material con el que se hizo la construcción exter
	'%     sother_constr   -   descripción de otros materiales de construcción
	'%     nstories        -   cantidad de niveles (pisos) que posee la construcción
	'%     nrooftype       -   tipo de techo.
	'%     nroofyear       -   año en que se realizó la  instalación o último  cambio al techo.
	'%     nhomesuper      -   superficie (expresada en mts2) de la vivienda.
	'%     nlandsuper      -   superficie (expresada en mts2) del terreno.
	'%     ngarage         -   cantidad de vehículos que pueden ocupar el estacio.
	'%     nfireplace      -   cantidad de chimeneas que se tienen en la vivienda
	'%     nbedrooms       -   cantidad de habitaciones que tiene la vivienda
	'%     nfullbath       -   cantidad de baños completos tiene la vivienda
	'%     nhalfbath       -   cantidad de medios baños que tiene la vivienda
	'%     nairtype        -   tipos de aire acondicionados
	'%     nalt_heating    -   código de sistema de calefacción.
	'%     sgas            -   indicador de poseer depósito de gasolina
	'%     ssprinksys      -   indicador de poseer sistema de riego
	'%     salarm_comp     -   nombre de la compañía que está a cargo del sistema  de alarma de la vivienda.
	'%     ndist_hydr      -   distancia al hidrante más cercano
	'%     snon_smok       -   indica que está permitido fumar en la vivienda
	'%     ndist_fire      -   distancia (expresada en km.) a la estación de bomberos más cerca.
	'%     sfiredepart     -   nombre de la estación de bomberos más cercana
	'%     nfloodzone      -   código de tipo de zonas de inundación
	'%     sfloodind       -   indicador de poseer o desear tener un seguro de  inundación
	'%     nswimpool       -   código de la ubicación de la piscina
	'%     sfencepool      -   indicador de piscina con cerca.
	'%     nfenceheight    -   altura de la cerca que posee la piscina
	'%     strampoline     -   indicador de pisina con trampolín
	'%     sanimalsind     -   indicador de poseer animales domésticos o ganado
	'%     sanimalsdes     -   descripción de los animales (cantidad, tipo y raza)
	'%     sattackedind    -   indicador.¿alguno de los animales ha atacado a alguien?
	'%     nfoundtype      -   código de fundaciones/cimientos/bases de la construcción.
    Public Function InsValHO001(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nDwellingType As Short, ByVal nOwnerShip As Short, ByVal nYear_built As Short, ByVal sCov_purc As String, ByVal nPrice_purch As Double, ByVal nCurrency_purch As Short, ByVal dDate_purch As Date, ByVal sPolicy_other As String, ByVal nCap_other As Double, ByVal nCurrency_other As Short, ByVal dExpir_other As Date, ByVal nExterConstr As Short, ByVal sOther_constr As String, ByVal nStories As Short, ByVal nRoofType As Short, ByVal nRoofYear As Short, ByVal nHomeSuper As Short, ByVal nLandSuper As Short, ByVal nGarage As Short, ByVal nFirePlace As Short, ByVal nBedrooms As Short, ByVal nFullBath As Short, ByVal nHalfBath As Short, ByVal nAirType As Short, ByVal nAlt_heating As Short, ByVal sGas As String, ByVal sSprinkSys As String, ByVal sAlarm_comp As String, ByVal nDist_Hydr As Short, ByVal sNon_smok As String, ByVal nDist_fire As Short, ByVal sFireDepart As String, ByVal nFloodZone As Short, ByVal nSeismicZone As Integer, ByVal sFloodInd As String, ByVal nSwimPool As Short, ByVal sFencePool As String, ByVal nFenceHeight As Short, ByVal sTrampoline As String, ByVal sAnimalsInd As String, ByVal sAnimalsDes As String, ByVal sAttackedInd As String, ByVal nFoundType As Short) As String
        Dim lclsErrors As eFunctions.Errors


        lclsErrors = New eFunctions.Errors

        If (nDwellingType = 0 Or nDwellingType = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 80102)
        End If
        If (nYear_built = 0 Or nYear_built = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3158)
        End If
        If (nOwnerShip = 0 Or nOwnerShip = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 80103)
        End If
        If sCov_purc = "1" And (nPrice_purch = 0 Or nPrice_purch = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 80104)
        End If
        If sCov_purc = "1" And (nPrice_purch <> 0 Or nPrice_purch <> eRemoteDB.Constants.intNull) And (nCurrency_purch = 0 Or nCurrency_purch = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 80105)
        End If
        If sPolicy_other = "1" And (nCap_other = 0 Or nCap_other = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 80107)
        End If
        If sPolicy_other = "1" And (nCap_other = 0 Or nCap_other = eRemoteDB.Constants.intNull) And (nCurrency_other = 0 Or nCurrency_other = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 80106)
        End If
        If sPolicy_other = "1" And (nCap_other = 0 Or nCap_other = eRemoteDB.Constants.intNull) And dExpir_other = eRemoteDB.Constants.dtmNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 80108)
        End If
        If nExterConstr = 99 And sOther_constr = String.Empty Then
            Call lclsErrors.ErrorMessage(sCodispl, 80109)
        End If
        If (nStories = 0 Or nStories = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 13750)
        End If
        If (nFoundType = 0 Or nFoundType = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 80110)
        End If
        If (nRoofType = 0 Or nRoofType = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 3223)
        End If
        If (nRoofYear = 0 Or nRoofYear = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 80111)
        End If
        If (nHomeSuper = 0 Or nHomeSuper = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 61930)
        End If
        If (nLandSuper = 0 Or nLandSuper = eRemoteDB.Constants.intNull) Then
            Call lclsErrors.ErrorMessage(sCodispl, 80113)
        End If
        If (nFenceHeight = 0 Or nFenceHeight = eRemoteDB.Constants.intNull) And sFencePool = "1" Then
            Call lclsErrors.ErrorMessage(sCodispl, 80114)
        End If

        InsValHO001 = lclsErrors.Confirm

        lclsErrors = Nothing

        Exit Function
    End Function
	
	'**%Objective: Pass of the information introduced towards the layers of rules of business and access of data.
	'**%Parameters:
	'**%     nHeader          -  Indicator of the zone (Header or detail).
	'**%     sCodispl         -  Logical code that identifies the transaction.
	'**%     nMainAction      -  Action being executed on the transaction.
	'**%     sAction          -  Action begin executed on the grid of the transaction
	'**%     nusercode        -  código del usuario
	'**%     scertype         -  type of registry
	'**%     nbranch          -  branch
	'**%     nproduct         -  product
	'**%     npolicy          -  i number of poliza
	'**%     ncertif          -  i number of certificate
	'**%     deffecdate       -  date of effect of the registry
	'**%     ndwellingtype    -  code of type of dwelling for homeowner policies
	'**%     nownership       -  code of ownership/occupation of a home
	'**%     nyear_built      -  year of construction
	'**%     scov_purc        -  it indicates the home purchase is covered
	'**%     nprice_purch     -  purchase price of the home
	'**%     ncurrency_purch  -  code of the currency of the purchase price
	'**%     ddate_purch      -  date of purchase.
	'**%     spolicy_other    -  it indicated that the risk has been covered by  another policy
	'**%     ncap_other       -  sum insured amount of the another policy
	'**%     ncurrency_other  -  code of the currency.
	'**%     dexpir_other     -  expiry date of the another policy.
	'**%     nexterconstr     -
	'**%     sother_constr    -  description of others materials of construcción.
	'**%     nstories         -  stories of the construcción
	'**%     nrooftype        -  type of roof
	'**%     nroofyear        -  year when the roof was installed or when it was last changed
	'**%     nhomesuper       -  area (mtrs2) of the home
	'**%     nlandsuper       -  area (mts2) of the land
	'**%     ngarage          -  quantity of cars in the garage
	'**%     nfireplace       -  quantity of fire places
	'**%     nbedrooms        -  quantity of  bedroom
	'**%     nfullbath        -  quantity of full bathrooms
	'**%     nhalfbath        -  quantity of half bathrooms
	'**%     nairtype         -  type of air conditioning
	'**%     nalt_heating     -  code of  alternate heating
	'**%     sgas             -  indicator to have gas tank
	'**%     ssprinksys       -  indicator to have sprinkler system
	'**%     salarm_comp      -  name of the company that monitors the alarm
	'**%     ndist_hydr       -  distance to the nearest fire hydrant
	'**%     snon_smok        -  it indicate that is allow to smoke in the dwelling
	'**%     ndist_fire       -  distance to nearest fire department
	'**%     sfiredepart      -  name of the nearest  fire department
	'**%     nfloodzone       -  flood zone type
	'**%     sfloodind        -  indicator of flood insurance
	'**%     nswimpool        -  code of  the  swimmig pool ubication
	'**%     sfencepool       -  it indicates that the swimming pool is enclosed  by a fence
	'**%     nfenceheight     -  height of the fence (in metros)
	'**%     strampoline      -  it indicates that swimming pool with trampoline
	'**%     sanimalsind      -  it indicates that the home has pets or livestock
	'**%     sanimalsdes      -  description of the animals.
	'**%     sattackedind     -  indicator,have any of these pets attacked anyone?
	'**%     nfoundtype       -  code of foundation
	'%Objetivo: Se encarga de llevar la información introducida hacia las capas de reglas de negocio y acceso de datos.
	'%Parámetros:
	'%     nHeader         -   Indicador de zona de encabezado o detalle
	'%     sCodispl        -   Código lógico que identifica la transacción.
	'%     nMainAction     -   Acción que se ejecuta sobre la transacción.
	'%     sAction         -   Acción que se ejecuta sobre el grid de la transacción.
	'%     nusercode       -   código del usuario
	'%     scertype        -   tipo de registro
	'%     nbranch         -   ramo
	'%     nproduct        -   producto
	'%     npolicy         -   numero de poliza
	'%     ncertif         -   numero de certificado
	'%     deffecdate      -   fecha de efecto del registro
	'%     ndwellingtype   -   código de tipo de vivienda para pólizas de hogar
	'%     nownership      -   código de ocupación de una vivienda
	'%     nyear_built     -   año de construcción de la vivienda
	'%     scov_purc       -   indica que se cubre el riesgo por adquisición  de casa
	'%     nprice_purch    -   precio de compra de la vivienda.
	'%     ncurrency_purch -   código de la moneda en la que está expresado el precio de compra.
	'%     ddate_purch     -   fecha de compra de la vivienda.
	'%     spolicy_other   -   indicador de que posee otra póliza cubriendo  el riesgo
	'%     ncap_other      -   monto de capital asegurado por la otra póliza
	'%     ncurrency_other -   código de la moneda en la que está expresado el monto asegurado de la otra póliza
	'%     dexpir_other    -   fecha de vencimiento de la otra póliza.
	'%     nexterconstr    -   material con el que se hizo la construcción exter
	'%     sother_constr   -   descripción de otros materiales de construcción
	'%     nstories        -   cantidad de niveles (pisos) que posee la construcción
	'%     nrooftype       -   tipo de techo.
	'%     nroofyear       -   año en que se realizó la  instalación o último  cambio al techo.
	'%     nhomesuper      -   superficie (expresada en mts2) de la vivienda.
	'%     nlandsuper      -   superficie (expresada en mts2) del terreno.
	'%     ngarage         -   cantidad de vehículos que pueden ocupar el estacio.
	'%     nfireplace      -   cantidad de chimeneas que se tienen en la vivienda
	'%     nbedrooms       -   cantidad de habitaciones que tiene la vivienda
	'%     nfullbath       -   cantidad de baños completos tiene la vivienda
	'%     nhalfbath       -   cantidad de medios baños que tiene la vivienda
	'%     nairtype        -   tipos de aire acondicionados
	'%     nalt_heating    -   código de sistema de calefacción.
	'%     sgas            -   indicador de poseer depósito de gasolina
	'%     ssprinksys      -   indicador de poseer sistema de riego
	'%     salarm_comp     -   nombre de la compañía que está a cargo del sistema  de alarma de la vivienda.
	'%     ndist_hydr      -   distancia al hidrante más cercano
	'%     snon_smok       -   indica que está permitido fumar en la vivienda
	'%     ndist_fire      -   distancia (expresada en km.) a la estación de bomberos más cerca.
	'%     sfiredepart     -   nombre de la estación de bomberos más cercana
	'%     nfloodzone      -   código de tipo de zonas de inundación
	'%     sfloodind       -   indicador de poseer o desear tener un seguro de  inundación
	'%     nswimpool       -   código de la ubicación de la piscina
	'%     sfencepool      -   indicador de piscina con cerca.
	'%     nfenceheight    -   altura de la cerca que posee la piscina
	'%     strampoline     -   indicador de pisina con trampolín
	'%     sanimalsind     -   indicador de poseer animales domésticos o ganado
	'%     sanimalsdes     -   descripción de los animales (cantidad, tipo y raza)
	'%     sattackedind    -   indicador.¿alguno de los animales ha atacado a alguien?
	'%     nfoundtype      -   código de fundaciones/cimientos/bases de la construcción.
    Public Function InsPostHO001(ByVal nHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal sCertype As String, ByVal nBranch As Short, ByVal nProduct As Short, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal dEffecdate As Date, ByVal nDwellingType As Short, ByVal nOwnerShip As Short, ByVal nYear_built As Short, ByVal sCov_purc As String, ByVal nPrice_purch As Double, ByVal nCurrency_purch As Short, ByVal dDate_purch As Date, ByVal sPolicy_other As String, ByVal nCap_other As Double, ByVal nCurrency_other As Short, ByVal dExpir_other As Date, ByVal nExterConstr As Short, ByVal sOther_constr As String, ByVal nStories As Short, ByVal nRoofType As Short, ByVal nRoofYear As Short, ByVal nHomeSuper As Short, ByVal nLandSuper As Short, ByVal nGarage As Short, ByVal nFirePlace As Short, ByVal nBedrooms As Short, ByVal nFullBath As Short, ByVal nHalfBath As Short, ByVal nAirType As Short, ByVal nAlt_heating As Short, ByVal sGas As String, ByVal sSprinkSys As String, ByVal sAlarm_comp As String, ByVal nDist_Hydr As Short, ByVal sNon_smok As String, ByVal nDist_fire As Short, ByVal sFireDepart As String, ByVal nFloodZone As Short, ByVal nSeismicZone As Integer, ByVal sFloodInd As String, ByVal nSwimPool As Short, ByVal sFencePool As String, ByVal nFenceHeight As Short, ByVal sTrampoline As String, ByVal sAnimalsInd As String, ByVal sAnimalsDes As String, ByVal sAttackedInd As String, ByVal nFoundType As Short) As Boolean

        Dim lclsPolicyWin As ePolicy.Policy_Win


        InsPostHO001 = Update(nUsercode, sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nDwellingType, nOwnerShip, nYear_built, sCov_purc, nPrice_purch, nCurrency_purch, dDate_purch, sPolicy_other, nCap_other, nCurrency_other, dExpir_other, nExterConstr, sOther_constr, nStories, nRoofType, nRoofYear, nHomeSuper, nLandSuper, nGarage, nFirePlace, nBedrooms, nFullBath, nHalfBath, nAirType, nAlt_heating, sGas, sSprinkSys, sAlarm_comp, nDist_Hydr, sNon_smok, nDist_fire, sFireDepart, nFloodZone, nSeismicZone, sFloodInd, nSwimPool, sFencePool, nFenceHeight, sTrampoline, sAnimalsInd, sAnimalsDes, sAttackedInd, nFoundType)

        If InsPostHO001 Then
            lclsPolicyWin = New ePolicy.Policy_Win
            Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "HO001", "2")
            lclsPolicyWin = Nothing
        End If

        Exit Function
    End Function
End Class












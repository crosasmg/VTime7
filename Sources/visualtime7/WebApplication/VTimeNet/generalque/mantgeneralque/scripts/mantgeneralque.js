//folderNode. Esta funcion se encarga de crear nuevos nodos. Retorna 
// un arreglo para ser concatenado en el arbol
//-------------------------------------------------------------------
function folderNode(name, scrClose, srcOpen,OpenFolder,Params,Key,ParentFolder){
//-------------------------------------------------------------------
   var arrayAux
   if (typeof(OpenFolder)=="undefined") OpenFolder=0
   if (OpenFolder>1) OpenFolder=1
   if (typeof (scrClose) == "undefined") scrClose = "/VTimeNet/images/clfolder.png"
   if (typeof (srcOpen) == "undefined") srcOpen = "/VTimeNet/images/Opfolder.png"
   if (typeof(Params)=="undefined") Params=''
   if (typeof(Key)=="undefined") Key=name
   arrayAux = new Array
   arrayAux[0] = OpenFolder
   arrayAux[1] = OpenFolder
   arrayAux[2] = 0
   if (scrClose == "") scrClose = "/VTimeNet/images/clfolder.png"
   arrayAux[3] = scrClose
   if (srcOpen == "") srcOpen = "/VTimeNet/images/Opfolder.png"
   arrayAux[4] = srcOpen
   arrayAux[5] = Params
   arrayAux[6] = Key
   arrayAux[7] = ParentFolder
   arrayAux[8] = name
   return arrayAux
}

//leafNode. Esta funcion se encarga de crear nuevas hojas. Retorna 
// un arreglo para ser concatenado en el arbol
//-------------------------------------------------------------------
function leafNode(name,scrClose,srcOpen,Params,Key,ParentFolder){
//-------------------------------------------------------------------
   var arrayAux
   if (typeof(scrClose)=="undefined") scrClose="/VTimeNet/images/clfolder.png"
   if (typeof(srcOpen)=="undefined") srcOpen="/VTimeNet/images/Opfolder.png"
   if (typeof(Params)=="undefined") Params=''
   if (typeof(Key)=="undefined") Key=name
   arrayAux = new Array
   arrayAux[0] = 0
   arrayAux[1] = 0
   arrayAux[2] = 1
   if (scrClose=="") scrClose="/VTimeNet/images/clfolder.png"
   arrayAux[3] = scrClose
   if (srcOpen=="") srcOpen="/VTimeNet/images/Opfolder.png"
   arrayAux[4] = srcOpen
   arrayAux[5] = Params
   arrayAux[6] = Key
   arrayAux[7] = ParentFolder
   arrayAux[8] = name
   return arrayAux
}

//leafNode. Esta funcion se encarga de crear nuevas hojas. Retorna 
// un arreglo para ser concatenado en el arbol
//-------------------------------------------------------------------
function leafNode2(name,scrClose,srcOpen,Params,Key,ParentFolder){
//-------------------------------------------------------------------
   var arrayAux
   if (typeof(scrClose)=="undefined") scrClose="/VTimeNet/images/clfolder.png"
   if (typeof(srcOpen)=="undefined") srcOpen="/VTimeNet/images/Opfolder.png"
   if (typeof(Params)=="undefined") Params=''
   if (typeof(Key)=="undefined") Key=name
   arrayAux = new Array
   arrayAux[0] = 0
   arrayAux[1] = 0
   arrayAux[2] = 2
   if (scrClose=="") scrClose="/VTimeNet/images/clfolder.png"
   arrayAux[3] = scrClose
   if (srcOpen=="") srcOpen="/VTimeNet/images/Opfolder.png"
   arrayAux[4] = srcOpen
   arrayAux[5] = Params
   arrayAux[6] = Key
   arrayAux[7] = ParentFolder
   arrayAux[8] = name
   return arrayAux
}

// Esta funcion se encarga de agregar hijos al nodo en tratemiento
//-------------------------------------------------------------------
function appendChild(parent, child){
//-------------------------------------------------------------------
   parent[parent.length] = child
   return child
}

//-------------------------------------------------------------------
function generateDocEntry(icon, docDescription, link){
//-------------------------------------------------------------------
        retString = "<A href='"+link+"' target=folderFrame><img src='/VTimeNet/images/menu_transaction.png' alt='Se abre en el marco de la derecha'"
    switch(icon) {
        case 1 : // Transacción
            retString = "<A href='"+link+"'><img src='/VTimeNet/images/menu_transaction.png' alt='Ir a la transacción'"
            break
        case 2 : // Consulta
            retString = "<A href='"+link+"'><img src='/VTimeNet/images/menu_query.png' alt='Ir a la transacción'"
            break
        case 3 : // Mantenimiento
            retString = "<A href='"+link+"'><img src='/VTimeNet/images/menu_maintance.png' alt='Ir a la transacción'"
            break
        case 4 : // Reportes
            retString = "<A href='"+link+"'><img src='/VTimeNet/images/Printer.png' alt='Ir a la transacción'"
            break
        case 5 : // Batchs
            retString = "<A href='"+link+"'><img src='/VTimeNet/images/batchStat03.png' alt='Ir a la transacción'"
            break
    }
    retString = retString + " border=0></a><td nowrap valign = middle ><font size=-1 face='Arial, Helvetica'><a href='" + link + "' style='text-decoration:none' >" + docDescription + "</a></font>"
    return retString
}

//-------------------------------------------------------------------
function redrawTree(){
//-------------------------------------------------------------------
    var lstrHTML = redrawNode(foldersTree, "<TABLE BORDER=0 WIDTH='100%'><TD>", 0, 1, "")
    lstrHTML = lstrHTML + "</TD></TABLE>"
    UpdateDiv("Treezone",lstrHTML,"normal")
}

//-------------------------------------------------------------------
function redrawNode(foldersNode, lstrHTML, level, lastNode, leftSide){
//-------------------------------------------------------------------
    var i=0
    lstrHTML =  lstrHTML + "<table border=0 cellspacing=0 cellpadding=0>" +
                "<tr><td valign = middle nowrap>" +
                leftSide 
    if (level>0)
        if (lastNode){
            lstrHTML =  lstrHTML + "<img src='/VTimeNet/images/lastnode.gif' width=16 height=22>"
            leftSide = leftSide + "<img src='/VTimeNet/images/blank.gif' width=16 height=22>" 
        }
        else {
            lstrHTML =  lstrHTML + "<img src='/VTimeNet/images/node.gif' width=16 height=22>"
            leftSide = leftSide + "<img src='/VTimeNet/images/vertline.gif' width=16 height=22>"
        }
    lstrHTML = displayIconAndLabel(foldersNode, lstrHTML)
    lstrHTML =  lstrHTML +  "</TABLE>"
    if (foldersNode.length > 9 && foldersNode[0]){
        if (!foldersNode[2]){
            level=level+1
            for (i=9; i<foldersNode.length;i++){
                if (i==foldersNode.length-1)
                    lstrHTML = redrawNode(foldersNode[i], lstrHTML, level, 1, leftSide)
                else
                    lstrHTML = redrawNode(foldersNode[i], lstrHTML, level, 0, leftSide)
            }
        }
        else {
            for (i=9; i<foldersNode.length;i++){
                lstrHTML = lstrHTML + "<table border=0 cellspacing=0 cellpadding=0 valign=center>" +
                           "<tr><td nowrap>" +
                           leftSide
                if (i==foldersNode.length - 1)
                    lstrHTML = lstrHTML + "<img src='/VTimeNet/images/lastnode.gif' width=16 height=22>"
                else
                    lstrHTML = lstrHTML + "<img src='/VTimeNet/images/node.gif' width=16 height=22>"
                lstrHTML = lstrHTML + foldersNode[i]
                lstrHTML = lstrHTML + "</table>"
            }
        }
    }
   return lstrHTML;
}

//-------------------------------------------------------------------
function displayIconAndLabel (foldersNode, lstrHTML){
//-------------------------------------------------------------------
    if (foldersNode[2]!=2){
		lstrHTML = lstrHTML + "<A href='javascript:openBranch(\"" + foldersNode[6] + "\")'"
		if (foldersNode[1]){
		    lstrHTML = lstrHTML + "onMouseOver='window.status=\"Contraer carpeta\"; return true'><img src=" +
		               foldersNode[4] + " width=24 height=22 border=noborder></a>"
		}
		else {
		    lstrHTML = lstrHTML + "onMouseOver='window.status=\"Abrir carpeta\"; return true'><img src=" +
		               foldersNode[3] + " width=24 height=22 border=noborder></a>"
		}
	}
    lstrHTML = lstrHTML + "<td valign=middle align=left nowrap>" +
	           "<font size=-1 face='Arial, Helvetica'>"+foldersNode[8]+"</font>"
    return lstrHTML;
}

//-------------------------------------------------------------------
function closeFolders(foldersNode){
//-------------------------------------------------------------------
    var i=0
    if (!foldersNode[2]){
        for (i=9; i< foldersNode.length; i++)
            closeFolders(foldersNode[i])
    }
    foldersNode[0] = 0
    foldersNode[1] = 0
    }
//	-------------------------------------------------------------------
function clickOnFolderRec(foldersNode, folderName){
//-------------------------------------------------------------------
    var i=0
    if (foldersNode[6] == folderName) {
//        if (foldersNode[0])
//            closeFolders(foldersNode)
//        else {
//            foldersNode[0] = 1
//            foldersNode[1] = 1
//        }
        CurrentFolder = foldersNode
//        insReloadFolder(foldersNode)
    }
    else {
        if (!foldersNode[2])
            for (i=9; i< foldersNode.length; i++)
                clickOnFolderRec(foldersNode[i], folderName)
    }
}

//-------------------------------------------------------------------
function openBranch(branchKey){
//-------------------------------------------------------------------
    clickOnFolderRec(foldersTree, branchKey)
    if (branchKey=="Start folder" && foldersTree[0]==0)
        location="basefldr.htm"
    timeOutId = setTimeout("redrawTree()",100)
}

//-------------------------------------------------------------------
function insReloadFolder(foldersNode){
//-------------------------------------------------------------------
   //document.location.href="GE099.asp?sReload=1&nParentFolder=" + foldersNode[7] + "&sKey=" + foldersNode[6] + foldersNode[5] + (foldersNode.length>9? '&nExistSubF=1':'&nExistSubF=2')
}
var foldersTree = 0
var timeOutId = 0
var CurrentFolder
// Deja de ocultar el contenido a los exploradores antiguos -->

//-------------------------------------------------------------------
function RemoveFolders(foldersNode,ParentFolder,Index){
//-------------------------------------------------------------------
    var i=0,Aux=0,AuxII=9
    var lblnContinue=true
    if (CurrentFolder==foldersTree)
        return false;
    else
    if (foldersNode[6]==CurrentFolder[6]){
       return false;
    }
    else{
        if (foldersNode.length<=9)
            return true;
        for (i=9; ((i< foldersNode.length)&&(lblnContinue)); i++){
            lblnContinue = RemoveFolders(foldersNode[i],foldersNode,i)
            if ((!lblnContinue)&&(foldersTree[6]!=CurrentFolder[6])){
                var ArrAux = new Array()
                for (Aux=0;Aux<foldersNode.length;Aux++){
                    if (Aux<9)
                        ArrAux[Aux]= foldersNode[Aux]
                    else{
                        if (foldersNode[Aux][6]!=CurrentFolder[6]){
                            foldersNode[Aux][6]
                            ArrAux[AuxII++]= foldersNode[Aux]
                        }
                    }
                }
                if (typeof(Index)=='undefined')
                    foldersTree = ArrAux
                else
                    ParentFolder[Index] = ArrAux
                CurrentFolder=foldersTree
            }
        }
    }
    return lblnContinue;
}

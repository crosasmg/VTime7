var foldersTree = 0
var timeOutId = 0
var CurrentFolder

//folderNode. Esta funcion se encarga de crear nuevos nodos. Retorna 
// un arreglo para ser concatenado en el arbol
//-------------------------------------------------------------------
function folderNode(name, scrClose, srcOpen, OpenFolder, Params, Key, ParentFolder) {
    //-------------------------------------------------------------------
    var arrayAux
    if (typeof (OpenFolder) == "undefined") OpenFolder = 0
    if (OpenFolder > 1) OpenFolder = 1
    if (typeof (scrClose) == "undefined") scrClose = "/vtimenet/images/clfolder.png"
    if (typeof (srcOpen) == "undefined") srcOpen = "/vtimenet/images/Opfolder.png"
    if (typeof (Params) == "undefined") Params = ''
    if (typeof (Key) == "undefined") Key = name
    arrayAux = new Array
    arrayAux[0] = OpenFolder
    arrayAux[1] = OpenFolder
    arrayAux[2] = 0
    if (scrClose == "") scrClose = "/vtimenet/images/clfolder.png"
    arrayAux[3] = scrClose
    if (srcOpen == "") srcOpen = "/vtimenet/images/Opfolder.png"
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
function leafNode(name, scrClose, srcOpen, Params, Key, ParentFolder) {
    //-------------------------------------------------------------------
    var arrayAux
    if (typeof (scrClose) == "undefined") scrClose = "/vtimenet/images/clfolder.png"
    if (typeof (srcOpen) == "undefined") srcOpen = "/vtimenet/images/Opfolder.png"
    if (typeof (Params) == "undefined") Params = ''
    if (typeof (Key) == "undefined") Key = name
    arrayAux = new Array
    arrayAux[0] = 0
    arrayAux[1] = 0
    arrayAux[2] = 1
    if (scrClose == "") scrClose = "/vtimenet/images/clfolder.png"
    arrayAux[3] = scrClose
    if (srcOpen == "") srcOpen = "/vtimenet/images/Opfolder.png"
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
function leafNode2(name, scrClose, srcOpen, Params, Key, ParentFolder) {
    //-------------------------------------------------------------------
    var arrayAux
    if (typeof (scrClose) == "undefined") scrClose = "/vtimenet/images/clfolder.png"
    if (typeof (srcOpen) == "undefined") srcOpen = "/vtimenet/images/Opfolder.png"
    if (typeof (Params) == "undefined") Params = ''
    if (typeof (Key) == "undefined") Key = name
    arrayAux = new Array
    arrayAux[0] = 0
    arrayAux[1] = 0
    arrayAux[2] = 2
    if (scrClose == "") scrClose = "/vtimenet/images/clfolder.png"
    arrayAux[3] = scrClose
    if (srcOpen == "") srcOpen = "/vtimenet/images/Opfolder.png"
    arrayAux[4] = srcOpen
    arrayAux[5] = Params
    arrayAux[6] = Key
    arrayAux[7] = ParentFolder
    arrayAux[8] = name
    return arrayAux
}

// Esta funcion se encarga de agregar hijos al nodo en tratemiento
//-------------------------------------------------------------------
function appendChild(parent, child) {
    //-------------------------------------------------------------------
    parent[parent.length] = child
    return child
}

//-------------------------------------------------------------------
function generateDocEntry(icon, docDescription, link) {
    //-------------------------------------------------------------------
    retString = "<A href='" + link + "' target=folderFrame><img src='/vtimenet/images/menu_transaction.png' alt='Se abre en el marco de la derecha'"
    switch (icon) {
        case 1: // Transacción
            retString = "<A href='" + link + "'><img src='/vtimenet/images/menu_transaction.png' alt='Ir a la transacción'"
            break
        case 2: // Consulta
            retString = "<A href='" + link + "'><img src='/vtimenet/images/menu_query.png' alt='Ir a la transacción'"
            break
        case 3: // Mantenimiento
            retString = "<A href='" + link + "'><img src='/vtimenet/images/menu_maintance.png' alt='Ir a la transacción'"
            break
        case 4: // Reportes
            retString = "<A href='" + link + "'><img src='/vtimenet/images/Printer.png' alt='Ir a la transacción'"
            break
        case 5: // Batchs
            retString = "<A href='" + link + "'><img src='/vtimenet/images/batchStat03.png' alt='Ir a la transacción'"
            break
    }
    retString = retString + " border=0></a><td nowrap valign = middle ><font size=-1 face='Arial, Helvetica'><a href='" + link + "' style='text-decoration:none' >" + docDescription + "</a></font>"
    return retString
}

//-------------------------------------------------------------------
function redrawTree() {
    //-------------------------------------------------------------------
    var doc = top.fraSequence.window.document
    doc.clear()
    doc.write("<SCRIPT> function insGoTo(RefUrl){" +
              'open(RefUrl, "Transaccion","toolbar=no,resizable=yes,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20");top.insClose()}' +
              "</SCRIPT>")
    //doc.write("<body VLINK='aqua' link='white' background='/vtimenet/images/frameSequence.jpg' STYLE=\"background-repeat: repeat-x; background-size: 330% 100%;color: #0064A6;padding-top: 85%;\" text='white'>")
    doc.write("<body VLINK='aqua' link='white' STYLE=\"background: linear-gradient(to right, rgb(0, 174, 239) -100%, rgb(255, 255, 255) 100%);color: #0064A6;\" text='white'><img src='/VTimeNet/Images/Logos/CompanyLogo.gif' STYLE=\"padding: 5%; padding-bottom: 20%; height: 150px; width: 180px;\">")
    //    doc.write("<body VLINK='aqua' link='white' BGCOLOR=navy background='/vtimenet/images/FrameModules.jpg' text='white'>")     
    //TEMP    doc.write("<body BGCOLOR='Ivory' LINK='navy' VLINK='navy'>")     
    redrawNode(foldersTree, doc, 0, 1, "")
    doc.close()
}

//-------------------------------------------------------------------
function redrawNode(foldersNode, doc, level, lastNode, leftSide) {
    //-------------------------------------------------------------------
    var i = 0
    doc.write("<table border=0 cellspacing=0 cellpadding=0>")
    doc.write("<tr><td valign = middle nowrap>")
    doc.write(leftSide)
    if (level > 0)
        if (lastNode) {
            doc.write("<img src='/vtimenet/images/lastnode.gif' width=16 height=22>")
            leftSide = leftSide + "<img src='/vtimenet/images/blank.gif' width=16 height=22>"
        }
        else {
            doc.write("<img src='/vtimenet/images/node.gif' width=16 height=22>")
            leftSide = leftSide + "<img src='/vtimenet/images/vertline.gif' width=16 height=22>"
        }
    displayIconAndLabel(foldersNode, doc)
    doc.write("</table>")
    if (foldersNode.length > 9 && foldersNode[0]) {
        if (!foldersNode[2]) {
            level = level + 1
            for (i = 9; i < foldersNode.length; i++) {
                if (i == foldersNode.length - 1)
                    redrawNode(foldersNode[i], doc, level, 1, leftSide)
                else
                    redrawNode(foldersNode[i], doc, level, 0, leftSide)
            }
        }
        else {
            for (i = 9; i < foldersNode.length; i++) {
                doc.write("<table border=0 cellspacing=0 cellpadding=0 valign=center>")
                doc.write("<tr><td nowrap>")
                doc.write(leftSide)
                if (i == foldersNode.length - 1)
                    doc.write("<img src='/vtimenet/images/lastnode.gif' width=16 height=22>")
                else
                    doc.write("<img src='/vtimenet/images/node.gif' width=16 height=22>")
                doc.write(foldersNode[i])
                doc.write("</table>")
            }
        }
    }
}

//-------------------------------------------------------------------
function displayIconAndLabel(foldersNode, doc) {
    //-------------------------------------------------------------------
    if (foldersNode[2] != 2) {
        doc.write("<A href='javascript:' onclick='top.fraHeader.openBranch(\"" + foldersNode[6] + "\"); return false' ")
        if (foldersNode[1]) {
            doc.write("onMouseOver='window.status=\"Contraer carpeta\"; return true'><img src=")
            doc.write(foldersNode[4] + " width=24 height=22 border=noborder></a>")
        }
        else {
            doc.write("onMouseOver='window.status=\"Abrir carpeta\"; return true'><img src=")
            doc.write(foldersNode[3] + " width=24 height=22 border=noborder></a>")
        }
    }
    doc.write("<td valign=middle align=left nowrap>")
    doc.write("<font size=-1 face='Arial, Helvetica'>" + foldersNode[8] + "</font>")
}

//-------------------------------------------------------------------
function closeFolders(foldersNode) {
    //-------------------------------------------------------------------
    var i = 0
    if (!foldersNode[2]) {
        for (i = 9; i < foldersNode.length; i++)
            closeFolders(foldersNode[i])
    }
    foldersNode[0] = 0
    foldersNode[1] = 0
}

//-------------------------------------------------------------------
function clickOnFolderRec(foldersNode, folderName) {
    //-------------------------------------------------------------------
    var i = 0
    if (foldersNode[6] == folderName) {
        if (foldersNode[0])
            closeFolders(foldersNode)
        else {
            foldersNode[0] = 1
            foldersNode[1] = 1
        }
        CurrentFolder = foldersNode
        insReloadFolder(foldersNode)
    }
    else {
        if (!foldersNode[2])
            for (i = 9; i < foldersNode.length; i++)
                clickOnFolderRec(foldersNode[i], folderName)
    }
}

//-------------------------------------------------------------------
function openBranch(branchKey) {
    //-------------------------------------------------------------------
    //	alert(branchKey);
    if (branchKey == 'N-3') {
        alert('Solo se mostrarán los 200 primeros certificados');
    }

    clickOnFolderRec(foldersTree, branchKey)
    if (branchKey == "Start folder" && foldersTree[0] == 0)
        top.folderFrame.location = "basefldr.htm"
    timeOutId = setTimeout("redrawTree()", 100)
}

//-------------------------------------------------------------------
function insReloadFolder(foldersNode) {
    //-------------------------------------------------------------------
    top.frames["fraFolder"].document.location.href = "GE099.aspx?sReload=1&nParentFolder=" + foldersNode[7] + "&sKey=" + foldersNode[6] + foldersNode[5] + (foldersNode.length > 9 ? '&nExistSubF=1' : '&nExistSubF=2')
}

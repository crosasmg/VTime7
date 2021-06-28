//folderNode. Esta funcion se encarga de crear nuevos nodos. Retorna 
// un arreglo para ser concatenado en el arbol
//-------------------------------------------------------------------
function folderNode(name, scrClose, srcOpen,OpenFolder){
//-------------------------------------------------------------------
   var arrayAux
   if (typeof(OpenFolder)=="undefined") OpenFolder=0
   if (OpenFolder>1) OpenFolder=1
   if (typeof(scrClose)=="undefined") scrClose="/VtimeNet/images/clfolder.png"
   if (typeof(srcOpen)=="undefined") srcOpen="/VtimeNet/images/Opfolder.png"
   if (scrClose=="") scrClose="/VtimeNet/images/clfolder.png"
   if (srcOpen=="") srcOpen="/VtimeNet/images/Opfolder.png"

   arrayAux = new Array
   arrayAux[0] = OpenFolder
   arrayAux[1] = OpenFolder
   arrayAux[2] = 0
   arrayAux[3] = scrClose
   arrayAux[4] = srcOpen
   arrayAux[5] = name
   return arrayAux
}

//leafNode. Esta funcion se encarga de crear nuevas hojas. Retorna 
// un arreglo para ser concatenado en el arbol
//-------------------------------------------------------------------
function leafNode(name,scrClose,srcOpen){
//-------------------------------------------------------------------
   var arrayAux
   if (typeof(scrClose)=="undefined") scrClose="/VtimeNet/images/clfolder.png"
   if (typeof(srcOpen)=="undefined") srcOpen="/VtimeNet/images/Opfolder.png"
   if (scrClose=="") scrClose="/VtimeNet/images/clfolder.png"
   if (srcOpen=="") srcOpen="/VtimeNet/images/Opfolder.png"

   arrayAux = new Array
   arrayAux[0] = 0
   arrayAux[1] = 0
   arrayAux[2] = 1
   arrayAux[3] = scrClose
   arrayAux[4] = srcOpen
   arrayAux[5] = name
   return arrayAux
}

//leafNode. Esta funcion se encarga de crear nuevas hojas. Retorna 
// un arreglo para ser concatenado en el arbol
//-------------------------------------------------------------------
function leafNode2(name,scrClose,srcOpen){
//-------------------------------------------------------------------
   var arrayAux
   if (typeof(scrClose)=="undefined") scrClose="/VtimeNet/images/clfolder.png"
   if (typeof(srcOpen)=="undefined") srcOpen="/VtimeNet/images/Opfolder.png"
   if (scrClose=="") scrClose="/VtimeNet/images/clfolder.png"
   if (srcOpen=="") srcOpen="/VtimeNet/images/Opfolder.png"

   arrayAux = new Array
   arrayAux[0] = 0
   arrayAux[1] = 0
   arrayAux[2] = 2
   arrayAux[3] = scrClose
   arrayAux[4] = srcOpen
   arrayAux[5] = name
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
function generateDocEntry(sCodispl, icon, docDescription, link){
//-------------------------------------------------------------------
    retString = " <A href='javascript:' ONCLICK='"+link+"' target=folderFrame><img src='/VtimeNet/images/menu_transaction.png' alt='Se abre en el marco de la derecha'"
    switch(icon) {
        case 1 : // Transacción
            retString = "  <A class='TransNode' href='javascript:' ONCLICK='"+link+"'><img class='WideNode' src='/VtimeNet/images/menu_transaction.png' alt='Ir a la transacción' OnmouseMove='MouseMoveImage(this, true)' OnmouseOut='MouseMoveImage(this, false)'"
            break
        case 2 : // Consulta
            retString = "  <A class='TransNode' href='javascript:' ONCLICK='"+link+"'><img class='WideNode' src='/VtimeNet/images/menu_query.png' alt='Ir a la transacción' OnmouseMove='MouseMoveImage(this, true)' OnmouseOut='MouseMoveImage(this, false)'"
            break
        case 3 : // Mantenimiento
            retString = "  <A class='TransNode' href='javascript:' ONCLICK='"+link+"'><img class='WideNode' src='/VtimeNet/images/menu_maintance.png' alt='Ir a la transacción' OnmouseMove='MouseMoveImage(this, true)' OnmouseOut='MouseMoveImage(this, false)'"
            break
        case 4 : // Reportes
            retString = "  <A class='TransNode' href='javascript:' ONCLICK='"+link+"'><img class='WideNode' src='/VtimeNet/images/Printer.png' alt='Ir a la transacción' OnmouseMove='MouseMoveImage(this, true)' OnmouseOut='MouseMoveImage(this, false)'"
            break
        case 5 : // Batchs
            retString = "  <A class='TransNode' href='javascript:' ONCLICK='"+link+"'><img class='WideNode' src='/VtimeNet/images/batchStat03.png' alt='Ir a la transacción' OnmouseMove='MouseMoveImage(this, true)' OnmouseOut='MouseMoveImage(this, false)'"
            break
        case 10 : // Transacción denegada
            retString = "  <A class='TransNode' href='javascript:' ONCLICK='"+link+"'><img class='WideNode' src='/VtimeNet/images/DeniedTr.png' alt='Transacción denegada' OnmouseMove='MouseMoveImage(this, true)' OnmouseOut='MouseMoveImage(this, false)'"
            break
    }
    retString = retString + "></A></td>\n  <td>" + 
                            "  <A class='TransNode' href='javascript:' ONCLICK='" + link + "' ONMOUSEMOVE='parent.window.status=\"Ir a " + docDescription + " \(" + sCodispl + "\) " + "\"' ONMOUSEOUT='parent.window.status=\"\"'>" + docDescription + "</A>"
    return retString
}

//-------------------------------------------------------------------
function redrawTree(){
//-------------------------------------------------------------------
    var doc = parent.treeFrame.window.document
        
    doc.clear()

    doc.writeln ("<HTML>")
    doc.writeln ("<HEAD>")
    doc.writeln("   <LINK REL='StyleSheet' TYPE='text/css' HREF='/VTimeNet/common/HorizontalRedAndWhite.css'>")
    

    doc.writeln ("<SCRIPT>")
    doc.writeln ("function MouseMoveImage(Field, OverImage){\n" + 
	           "    parent.window.status = (OverImage)?Field.alt:'';}")

    doc.writeln ("function insGoTo(RefUrl){\n" +
               "    var win=open(RefUrl, 'Transaccion','toolbar=no,resizable=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');\n    win.moveTo(0, 0);\n    win.resizeTo(window.screen.availWidth, window.screen.availHeight);\n    parent.insClose()\n}");
    
    doc.writeln ("function insGoToCodispl(sCodispl, sParameters){\n" +
                 "    sParameters = ((sParameters!='')?'&':'') + sParameters;\n" +
                 "    insGoTo('/VTimeNet/common/GoTo.asp?sCodispl=' + sCodispl + sParameters);\n}");
    doc.writeln ("</SCRIPT>")
               
    doc.writeln ("</HEAD>")

    doc.writeln ("<BODY>")
    
    redrawNode(foldersTree, doc, 0, 1, "")
    
    doc.writeln ("</BODY>\n</HTML>")
    
    doc.close();
    
}

//-------------------------------------------------------------------
function redrawNode(foldersNode, doc, level, lastNode, leftSide){
//-------------------------------------------------------------------
    var i=0
	var objfoldersNodelength = foldersNode.length;
    doc.writeln("<table border=0 cellspacing=0 cellpadding=0>")

    doc.write("<tr valign=top>")
    doc.writeln(leftSide)

    if (level>0)
        if (lastNode){
            doc.writeln("  <td><img src='/VtimeNet/images/lastnode.gif' class='ThinNode'></td>")
            leftSide = leftSide + "  <td><img src='/VtimeNet/images/blank.gif' class='ThinNode'></td>" 
        }
        else {
            doc.writeln("  <td><img src='/VtimeNet/images/node.gif' class='ThinNode'></td>")
            leftSide = leftSide + "  <td><img src='/VtimeNet/images/vertline.gif' class='ThinNode'></td>"
        }
    displayIconAndLabel(foldersNode, doc)

    doc.writeln("</tr></table>")
    doc.writeln("")
    
//+Si hay hijos y se deben mostrar    
    if (foldersNode.length > 6 && foldersNode[0]){

//+Si es nodo carpeta
        if (!foldersNode[2]){
            level=level+1
            for (i=6; i<objfoldersNodelength;i++){
                if (i==objfoldersNodelength-1)
                    redrawNode(foldersNode[i], doc, level, 1, leftSide)
                else
                    redrawNode(foldersNode[i], doc, level, 0, leftSide)
            }
        }   
        else {
            for (i=6; i<objfoldersNodelength;i++){
                doc.writeln("<table border=0 cellspacing=0 cellpadding=0>")
                doc.writeln("<tr>\n  <td nowrap>")    
                doc.writeln(leftSide)
                if (i==objfoldersNodelength - 1)
                    doc.write("  <img src='/VtimeNet/images/lastnode.gif' class='ThinNode'>")
                else
                    doc.write("  <img src='/VtimeNet/images/node.gif' class='ThinNode'>")
                doc.writeln(foldersNode[i])
                doc.writeln("</tr></table>")
            }
        }
    }
}

//-------------------------------------------------------------------
function displayIconAndLabel (foldersNode, doc){
//-------------------------------------------------------------------

    if (foldersNode[2]!=2){
		doc.write("  <td><A class='TransNode' href='javascript:parent.FraHeader.openBranch(\"" + foldersNode[5] + "\")' ")

		if (foldersNode[1]){
		    doc.write("onMouseOut='parent.window.status=\"\"' onMouseOver='window.status=\"Contraer carpeta\"; return true'>")
		    doc.write("<img src='" + foldersNode[4] + "' class='WideNode'></a>")
		}
		else {
		    doc.write("onMouseOut='parent.window.status=\"\"' onMouseOver='window.status=\"Abrir carpeta\"; return true'>")
		    doc.write("<img src='" + foldersNode[3] + "' class='WideNode'></a>")
		}
		doc.writeln("</td>")
	}

    doc.writeln("  <td class='TransNode'>" + foldersNode[5] + "</td>")
}

//-------------------------------------------------------------------
function closeFolders(foldersNode){
//-------------------------------------------------------------------
    var i=0
    var objfoldersNodelength = foldersNode.length
    if (!foldersNode[2]){
        for (i=6; i< objfoldersNodelength; i++)
            closeFolders(foldersNode[i])
    }
    foldersNode[0] = 0
    foldersNode[1] = 0
    }

//-------------------------------------------------------------------
function clickOnFolderRec(foldersNode, folderName){
//-------------------------------------------------------------------
    var i=0
    var objfoldersNodelength = foldersNode.length
    if (foldersNode[5] == folderName) {
        if (foldersNode[0])
            closeFolders(foldersNode)
        else {
            foldersNode[0] = 1
            foldersNode[1] = 1
        }
    }
    else {
        if (!foldersNode[2])
            for (i=6; i< objfoldersNodelength; i++)
                clickOnFolderRec(foldersNode[i], folderName)
    }
}

//-------------------------------------------------------------------
function openBranch(branchName){
//-------------------------------------------------------------------
    clickOnFolderRec(foldersTree, branchName)
    if (branchName=="Start folder" && foldersTree[0]==0)
        parent.folderFrame.location="basefldr.htm"
    timeOutId = setTimeout("redrawTree()",100)
}
var foldersTree = 0
var timeOutId = 0


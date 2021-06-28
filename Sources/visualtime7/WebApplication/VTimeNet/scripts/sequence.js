//--------------------------------------------------------------------
//- $$Workfile: sequence.js $ 
//- $$Author: Nvaplat7 $ 
//- $$Date: 31/10/03 20:04 $ 
//- $$Revision: 4 $
//--------------------------------------------------------------------
var mstrStyleSheet = '';

//-------------------------------------------------------------------------------------------
// Sequence.js: Este archivo contiene las funciones utilizadas por las secuencias de ventanas de la aplicación
//-------------------------------------------------------------------------------------------

//% FindWindows: Busca una ventana y retorna el índice
//-------------------------------------------------------------------------------------------
function FindWindows(WinName, bUpdImage){
//-------------------------------------------------------------------------------------------
    var lintIndex = -1;
    var lstrURL = top.fraFolder.document.location.href;
    var lintIndexPage = -1;
    var lintIndexChild;
    var aux  = '';
    var aux2 = '';

    if (typeof(bUpdImage) == 'undefined') bUpdImage = false;

    if (WinName != ''){
        if(WinName.indexOf('CA014')>-1){
			if (!bUpdImage){
				if(lstrURL.indexOf('nIndexCover')>-1){
				    aux  = lstrURL.substr(lstrURL.indexOf('nIndexCover=') + 12, lstrURL.length);
				    aux2 = aux.substr(0,aux.indexOf('&'));
				    if (aux2 != ''){
				        lintIndexPage = aux2;
				    }
				    else{
				        lintIndexPage = lstrURL.substr(lstrURL.indexOf('nIndexCover=') + 12, lstrURL.length);
				    }
				     
				}
			}
            else{
				lintIndexPage = WinName.substr(WinName.indexOf('CA014') + 5);
            }
        }

        do {
            ++lintIndex
			if (typeof(sequence[lintIndex]) != 'undefined') lstrURL = sequence[lintIndex].Page;
            if (lintIndex < sequence.length){
                if (lstrURL.search('Codispl=' + 'CA014' + '&') != -1){
				    aux  = lstrURL.substr(lstrURL.indexOf('nIndexCover=') + 12, lstrURL.length);
				    aux2 = aux.substr(0,aux.indexOf('&'));
				    if (aux2 != ''){
				        lintIndexChild = aux2;
				    }
				    else{
				        lintIndexChild = lstrURL.substr(lstrURL.indexOf('nIndexCover=') + 12, lstrURL.length);
				    }
                }
			}
        }
        while ((lintIndex < mintWinCount) && 
              ((lstrURL.search('Codispl=' + WinName + '&') == -1) && 
              ((lstrURL.indexOf('CA014')<=-1) ||
              (lintIndexChild!=lintIndexPage))))
    }
    else
        lintIndex = -1;

    return(lintIndex);
}

//% NextWindows: se realiza la navegación entre las ventanas de la secuencia
//-------------------------------------------------------------------------------------------
function NextWindows(WinName){
//-------------------------------------------------------------------------------------------
    var lintIndex;
    var lintReqIndex = 0;

	lintIndex = FindWindows(WinName);
    if (lintIndex > -1){

    	if (++lintIndex < mintWinCount) {

    		for (lintReqIndex = lintIndex; lintReqIndex < mintWinCount; lintReqIndex++) {
    			switch (sequence[lintReqIndex].Require) {
    				case '2':
    					lintIndex = lintReqIndex;
    					lintReqIndex = mintWinCount + 1
    					break;

    				case '3':
    					lintIndex = ++lintReqIndex;
    					if (lintIndex >= mintWinCount)
    						lintIndex = 0;
    					while (sequence[lintIndex].Require == '3' && lintIndex < mintWinCount) {
    						lintIndex++;
    						if (lintIndex >= mintWinCount) {
    							lintIndex = 0;
    							break;
    						}
    					}

    					lintReqIndex = mintWinCount + 1
    					break;

    				case '6':
    					lintIndex = lintReqIndex;
    					lintReqIndex = mintWinCount + 1
    					break;
    			}

    			top.fraFolder.document.location = sequence[lintIndex].Page
    		}

    	}
    	else
    		if (typeof (sequence[0]) != 'undefined') {


    			top.fraFolder.document.location = sequence[0].Page
    		}
    		else {

    			//alert('No existe secuencia asociada');
    			alert(resValues.undefinedSequenceMessage);
    		}
    }
    else {
		
			
        top.fraFolder.document.location = sequence[0].Page;
    } 
}




//-------------------------------------------------------------------
function clickOnFolderRec(foldersNode, folderName){
//-------------------------------------------------------------------
    var i=0
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
            for (i=6; i< foldersNode.length; i++)
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

//initializeTree: Inicializa el arbol
//-------------------------------------------------------------------
function initializeTree(sStyleSheet) {
//-------------------------------------------------------------------
    mstrStyleSheet = sStyleSheet;
    generateTree()
    redrawTree()
}

//folderNode. Esta funcion se encarga de crear nuevos nodos. Retorna 
// un arreglo para ser concatenado en el arbol
//-------------------------------------------------------------------
function folderNode(name, scrClose, srcOpen,OpenFolder){
//-------------------------------------------------------------------
   var arrayAux
   if (typeof(OpenFolder)=="undefined") OpenFolder=0
   if (OpenFolder>1) OpenFolder=1
   if (typeof(scrClose)=="undefined") scrClose="/VTimeNet/images/clfolder.png"
   if (typeof(srcOpen)=="undefined") srcOpen="/VTimeNet/images/Opfolder.png"
   arrayAux = new Array
   arrayAux[0] = OpenFolder
   arrayAux[1] = OpenFolder
   arrayAux[2] = 0
   if (scrClose=="") scrClose="/VTimeNet/images/clfolder.png"
   arrayAux[3] = scrClose
   if (srcOpen=="") srcOpen="/VTimeNet/images/Opfolder.png"
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
   if (typeof(scrClose)=="undefined") scrClose="/VTimeNet/images/clfolder.png"
   if (typeof(srcOpen)=="undefined") srcOpen="/VTimeNet/images/Opfolder.png"
   arrayAux = new Array
   arrayAux[0] = 0
   arrayAux[1] = 0
   arrayAux[2] = 1
   if (scrClose=="") scrClose="/VTimeNet/images/clfolder.png"
   arrayAux[3] = scrClose
   if (srcOpen=="") srcOpen="/VTimeNet/images/Opfolder.png"
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
   if (typeof(scrClose)=="undefined") scrClose="/VTimeNet/images/clfolder.png"
   if (typeof(srcOpen)=="undefined") srcOpen="/VTimeNet/images/Opfolder.png"
   arrayAux = new Array
   arrayAux[0] = 0
   arrayAux[1] = 0
   arrayAux[2] = 2
   if (scrClose=="") scrClose="/VTimeNet/images/clfolder.png"
   arrayAux[3] = scrClose
   if (srcOpen=="") srcOpen="/VTimeNet/images/Opfolder.png"
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

//%UpdImageContent: Actualiza la imagen a asociar a la transacción
//-------------------------------------------------------------------
function UpdImageContent(ImgName, sContent, nIndex){
//-------------------------------------------------------------------
	var lblnChecked;
	var lstrImage;
	var lstrAlt;
	var lstrRequired;
	var Require = '0';

//+ Los posibles valores de sContent son:
//+      1 -  Sin Contenido
//+      2 -  Con Contenido
//+      3 -  Sin Contenido y Requerida para la poliza/certificado
//+      4 -  Sin Contenido y No requerida para la poliza/certificado
//+      5 -  Con Contenido y Requerida para la poliza/certificado
//+      6 -  Con Contenido y No requerida para la poliza/certificado
    switch(sContent) {
        case '1' :
 			lstrRequired = sequence[nIndex].sRequired;
			lblnChecked = false;
            break;

        case '2' :
			lstrRequired = sequence[nIndex].sRequired;
			lblnChecked = true;
            break;

        case '3' :
			lstrRequired  = '1';
			lblnChecked = false;
            break;

        case '4' :
			lstrRequired  = '2';
			lblnChecked = false;
            break;

        case '5' :
			lstrRequired  = '1';
			lblnChecked = true;
            break;

        case '6' :
			lstrRequired  = '2';
			lblnChecked = true;
	}

	if (lblnChecked){
		lstrImage = '/VTimeNet/images/Checked.png';
		lstrAlt   = 'Con contenido';
		Require   = '1';
	}
	else
		if (lstrRequired == '1'){
			lstrImage = '/VTimeNet/images/NotChecked.png';
			lstrAlt   = 'Requerida/Sin contenido';
			Require   = '2';
		}
		else{
			lstrImage = '/VTimeNet/images/Empty.png';
			lstrAlt   = 'Sin contenido';
		}

	if (typeof(top.TreeSequence.document.images[ImgName])!='undefined'){
		top.TreeSequence.document.images[ImgName].src = lstrImage;
		top.TreeSequence.document.images[ImgName].alt = lstrAlt;
		sequence[nIndex].Require = Require;
	}
}

//%UpdContent: Actualiza el indicador de contenido de una ventana
//-------------------------------------------------------------------
function UpdContent(WinName, sContent){
//-------------------------------------------------------------------
    var lintIndex;
    
	lintIndex = FindWindows(WinName, true);
	if (typeof(sequence[lintIndex]) != 'undefined')
		if(sequence[lintIndex].Require != '3' && sequence[lintIndex].Require != '6')
			UpdImageContent(WinName, sContent, lintIndex);
}

//-------------------------------------------------------------------
function generateDocEntry(icon, docDescription, link, sDescript, sCodispl){
//-------------------------------------------------------------------
    var lstrDescript = '"'+ sDescript +'"';
    //var lstrMessage = '"'+ 'La ventana no está permitida para su esquema de seguridad' +'"';
    var lstrMessage = '"' + resValues.notAllowedWinSecuritySchemaMessage + '"';
    retString = "<A href='"+link+"' ONMOUSEMOVE='top.window.status="+ lstrDescript + "' ONMOUSEOUT=top.window.status=''><img src='/VtimeNet/images/Empty.png' alt='Sin contenido'"
   
    switch(icon) {
        case 1 : // eOK
            retString = "<A href='"+link+"' ONMOUSEMOVE='top.window.status="+ lstrDescript + "' ONMOUSEOUT=top.window.status=''><img src='/VtimeNet/images/Checked.png' alt='Con contenido'"
            break
        case 2 : // eRequired
            retString = "<A href='" + link + "' ONMOUSEMOVE='top.window.status=" + lstrDescript + "' ONMOUSEOUT=top.window.status=''><img src='/VtimeNet/images/NotChecked.png' alt='Requerida/Sin contenido'"
            break
        case 3 : // eDeniedS
        case 4 : // eDeniedOK
        case 5 : // eDeniedReq


            retString = "<A  ONCLICK='" + "alert(" + lstrMessage + ");" + "' ONMOUSEMOVE='top.window.status=" + lstrDescript + "' ONMOUSEOUT=top.window.status=''><img src='/VtimeNet/images/DeniedTr.png' alt='Transacción denegada'"
            break
        case 6 : // eOnlyQuery
            retString = "<A href='" + link + "' ONMOUSEMOVE='top.window.status=" + lstrDescript + "' ONMOUSEOUT=top.window.status=''><img src='/VtimeNet/images/FindPolicyOff.png' alt='Transacción denegada/Solo consulta'"
            break
    }

    if (icon==3 || icon==4 || icon==5)
	retString = retString + " NAME='" + sCodispl + "' border=0></a></td>\n    <td nowrap class='folderSeq'><a class='linkSeq'  ONCLICK='" + "alert(" + lstrMessage + ");" + "' ONMOUSEMOVE='top.window.status="+ lstrDescript +  "' ONMOUSEOUT=top.window.status=''>" + docDescription + "</a>"
    else
	retString = retString + " NAME='" + sCodispl + "' border=0></a></td>\n    <td nowrap class='folderSeq'><a class='linkSeq' href='" + link + "' ONMOUSEMOVE='top.window.status="+ lstrDescript +  "' ONMOUSEOUT=top.window.status=''>" + docDescription + "</a>"
    
    
    return retString
}

//-------------------------------------------------------------------
function redrawTreeSequence(doc){
//-------------------------------------------------------------------
    doc.writeln("<HEAD>")
    doc.writeln("<BASE TARGET='fraFolder'>")
    doc.writeln("<LINK REL='StyleSheet' TYPE='text/css' HREF='/VTimeNet/common/" + mstrStyleSheet + ".css'>")
    doc.writeln("</HEAD>")
    doc.writeln("<BODY id='left_frame'>")
    doc.writeln("<img NAME=logo src='/VTimeNet/images/logo.gif' ALT='Logo de la empresa' WIDTH='121' HEIGHT='96'><BR><BR>")
}

//-------------------------------------------------------------------
function redrawTree(){
//-------------------------------------------------------------------
    var doc = top.TreeSequence.window.document
    doc.clear()
    redrawTreeSequence(doc)
    redrawNode(foldersTree, doc, 0, 1, "")
    doc.close()
}

//-------------------------------------------------------------------
function redrawNode(foldersNode, doc, level, lastNode, leftSide){
//-------------------------------------------------------------------
    var i=0
    doc.writeln("<table border=0 cellspacing=0 cellpadding=0>")
    doc.writeln("  <tr>")
    doc.write  ("    <td>")
    doc.write(leftSide)
    if (level>0)
        if (lastNode){
            doc.write("<img src='/VTimeNet/images/lastnode.gif' width=16 height=22>")
            leftSide = leftSide + "<img src='/VTimeNet/images/blank.gif' width=16 height=22>" 
        }
        else {
            doc.write("<img src='/VTimeNet/images/node.gif' width=16 height=22>")
            leftSide = leftSide + "<img src='/VTimeNet/images/vertline.gif' width=16 height=22>"
        }
    displayIconAndLabel(foldersNode, doc)
    doc.writeln("</td>\n  </tr>")
    doc.writeln("</table>")
    if (foldersNode.length > 6 && foldersNode[0]){
        if (!foldersNode[2]){
            level=level+1
            for (i=6; i<foldersNode.length;i++){
                if (i==foldersNode.length-1)
                    redrawNode(foldersNode[i], doc, level, 1, leftSide)
                else
                    redrawNode(foldersNode[i], doc, level, 0, leftSide)
            }
        }
        else {
            for (i=6; i<foldersNode.length;i++){
                doc.writeln("<table border=0 cellspacing=0 cellpadding=0 valign=center>")
                doc.writeln("  <tr>")
                doc.write  ("    <td>")
                doc.write(leftSide)
                if (i==foldersNode.length - 1)
                    doc.write("<img src='/VTimeNet/images/lastnode.gif' width=16 height=22>")
                else
                    doc.write("<img src='/VTimeNet/images/node.gif' width=16 height=22>")
                doc.write(foldersNode[i])
                doc.writeln("</td>\n  </tr>")
                doc.writeln("</table>")
            }
        }
    }
}

//-------------------------------------------------------------------
function displayIconAndLabel (foldersNode, doc){
//-------------------------------------------------------------------
    if (foldersNode[2]!=2){
        doc.write("<A href='javascript:top.fraSequence.openBranch(\"" + foldersNode[5] + "\")'")
        if (foldersNode[1]){
            doc.write("onMouseOver='window.status=\"Contraer carpeta\"; return true'><img src=")
            doc.write(foldersNode[4] + " width=24 height=22 border=noborder></a>")
        }
        else {
            doc.write("onMouseOver='window.status=\"Abrir carpeta\"; return true'><img src=")
            doc.write(foldersNode[3] + " width=24 height=22 border=noborder></a>")
        }
    }
    doc.writeln("</td>")
    doc.write("    <td nowrap align=left class='folderSeq'>")
    doc.write(foldersNode[5])
}

//-------------------------------------------------------------------
function closeFolders(foldersNode){
//-------------------------------------------------------------------
    var i=0
    if (!foldersNode[2]){
        for (i=6; i< foldersNode.length; i++)
            closeFolders(foldersNode[i])
    }
    foldersNode[0] = 0
    foldersNode[1] = 0
    }

var foldersTree = 0
var timeOutId = 0

// Deja de ocultar el contenido a los exploradores antiguos -->

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RxListNews.aspx.cs" Inherits="fasi_widgets_RxListNews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/fasi/assets/jstree/dist/themes/default/style.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/fasi/assets/css/fasi.mp.fw.bundle.css" />
    <link rel="stylesheet" href="/fasi/assets/css/dist/css/bootstrap.css" />
    <link rel="stylesheet" href="/fasi/assets/font-awesome/css/font-awesome.css" />
</head>
<body>
    <div style="background-color:white;">
        <div style="padding-bottom:15px;">
            <img src="https://images.rxlist.com/images/mobile/rxlist/logo-rxl.png" alt="RxList" />
        </div>
        <div id="news"></div>
    </div>
    <script src="/fasi/assets/js/jquery-1.11.3.min.js"></script>
    <script src="/fasi/assets/jstree/dist/jstree.min.js"></script>
    <script>
        $(document).ready(function () {
            var url = window.location.search.substring(1);
            var qArray = url.split('&');
            for (var i = 0; i < qArray.length; i++) 
            {
                var pArr = qArray[i].split('=');
                var params = JSON.parse( decodeURIComponent(pArr[1]));
            }
            var data = JSON.stringify({ limit: params.count })
            $.ajax({
                async: true,
                url: "RxListNews.aspx/LoadNews",
                type: 'POST',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: data,
                success: function (result) {
                    items = BuildTreeItems(result.d);
                    $('#news').jstree({
                        "plugins": ["wholerow"],
                        'core': {
                            'themes': { 'name': 'default' },
                            "check_callback": true,
                            'data': items
                        }
                    }).on("changed.jstree", function (e, data) {
                        if (data.selected.length) {
                            var node = data.instance.get_node(data.selected[0]);
                            window.open(node.data.url, '_blank');
                        }
                    });
                }     
            });
        });
        // Crea los artículos del arból del menú
        function BuildTreeItems(data) {
            var items = new Array();
            $.each(data, function (index, item) {
                var treeNode = {
                    data: { url: item },
                    text: index,
                    icon: 'fa fa-newspaper-o',
                    a_attr: item.Path
                };
                items.push(treeNode);
            });
            return items;
        };
    </script>
</body>
</html>

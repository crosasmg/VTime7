<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="fasi_utils_Index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Bootstrap Theme The Band</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="/fasi/assets/css/bootstrap.min.css">
    <script src="/fasi/assets/js/jquery-1.11.3.min.js"></script>
    <script src="/fasi/assets/js/bootstrap.min.js"></script>

    <script>
        $(document).ready(function () {
            $("#btnCall").click(function (e) {
                $.ajax({
                    url: 'Index.aspx/Operation',
                    type: 'POST',
                    async: false,
                    data: JSON.stringify({
                        operation: "call",
                        body: JSON.stringify({})
                    }),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8"
                }).done(function (data) {
                    console.trace(data.d);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    serrorFunction();
                });
            });
        });
    </script>
</head>
<body>

    <div class="container">
        <br />
        <button type="button" id="btnCall" class="btn btn-primary">Call</button>
    </div>
</body>
</html>
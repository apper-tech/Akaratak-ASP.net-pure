﻿<html>
<head><title></title></head>
<body>
    <link rel="stylesheet" href="CustomDesign/css/bootstrap.css">
    <link rel="stylesheet" href="CustomDesign/css/asBreadcrumbs.css">
    <script src="CustomDesign/js/jquery.js"></script>
    <script src="CustomDesign/js/bootstrap.min.js"></script>
    <script src="CustomDesign/js/jquery-asBreadcrumbs.js"></script>
    <ol class="breadcrumb">
        <li><a href="#">Home</a></li>
        <li><a href="#">Getting Started</a></li>
        <li><a href="#">Library</a></li>
        <li><a href="#">Document</a></li>
        <li><a href="#">Components</a></li>
        <li><a href="#">JavaScript</a></li>
        <li><a href="#">Customize</a></li>
        <li class="active">Data</li>
    </ol>
    <script>
        jQuery(function ($) {
            $('.breadcrumb').asBreadcrumbs();
        });
    </script>
</body>
</html>

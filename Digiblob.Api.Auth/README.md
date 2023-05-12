<h1>Digiblob</h1>

<h2>Prerequisites</h2>

<h3>Docker</h3>

<hr class="rounded">

<!-- Serilog Sinks -->

<h4>Seq <span class="ribbon">NEW</span></h4>
<p>docker pull datalust/seq</p>
<p>docker run --name <b>CONTAINER_NAME</b> -d --restart unless-stopped -e ACCEPT_EULA=Y -v <b>FOLDER_PATH</b>:/data -p <b>PORT</b>:<b>PORT</b> -d <b>DOCKER_IMAGE_NAME</b></p>

<h5>Example</h5>
<p>docker run --name <b>Seq</b> -d --restart unless-stopped -e ACCEPT_EULA=Y -v <b>D:/Digiblob</b>:/data -p <b>8001</b>:<b>80</b> -d <b>datalust/seq</b></p>

<hr class="rounded">

<!-- Microsoft SQL Server -->

<h4>SQL Server <span class="ribbon">NEW</span></h4>
<p>docker pull mcr.microsoft.com/mssql/server</p>
<p>docker run --name <b>CONTAINER_NAME</b> -e ACCEPT_EULA=Yes -e SA_PASSWORD=<b>SUPER_ADMIN_PASSWORD</b> -p <b>PORT</b>:<b style>PORT</b> -d <b>DOCKER_IMAGE_NAME</b></p>

<h5>Example</h5>
<p>docker run --name <b>sqlserver</b> -e ACCEPT_EULA=Yes -e <b>SA_PASSWORD=Pass123$</b> -p <b>1433</b>:<b>1433</b> -d <b>mcr.microsoft.com/mssql/server</b></p>

<hr class="rounded">

<style>
    h1, h2, h3, h4 {
        font-weight: bold;
        color: rgb(200,200,150);
    }

    b {
        color:palevioletred;
        
    }

    .btn {
      border: none;
      border-radius: 5px;
      padding: 12px 20px;
      font-size: 16px;
      cursor: pointer;
      background-color: #282A35;
      color: white;
      position: relative;
    }

    .ribbon {
      width: 60px;
      font-size: 14px;
      padding: 4px;
      position: absolute;
      right: -25px;
      top: -12px;
      text-align: center;
      border-radius: 25px;
      transform: rotate(20deg);
      background-color: #ff9800;
      color: white;
    }
</style>

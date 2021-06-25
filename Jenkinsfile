pipeline {
    
    /*
     -- WebApplicationJenkins
        -- a 项目(类库)
        -- b 项目(类库)
        -- Web 项目
           -- Web.csproj
        -- WebApplicationJenkins.sln
    */

    // 全局环境变量
    environment {
        // 项目配置
        SLNNAME    =   'WebApplicationJenkins.sln'                                   // 解决方案 .sln 文件的位置
        PROJNAME   =   'WebApplicationJenkins/Web.csproj'          // 主项目的 .csproj 文件位置
        PUBXML     =   'WebApplicationJenkins/Properties/PublishProfiles/FolderProfile.pubxml'    // pubxml 文件位置
        PUBWEBDIR  =   'C:\\jenkinsnetfx'                       // pubxml 文件 <PublishUrl>C:\test</PublishUrl>，即编译后文件的存放目录

        // IIS 配置
        IISAPP     =   'abcd'                           // 网站名称
        IISADDR    =   'https://192.168.0.78:8172'      // WebDeploy 的地址
        IISUSER    =   'admin'                          // 用于登录到 IIS 的账号密码
        IISADMIN   =   'dev123456'                              
    }

  // .NET Framework 的程序请选择 windows 节点构建流水线
  agent {
    node {
      label 'windows'
    }

  }

    stages {

    stage('Init') {
      steps {
        sh 'env'
      }
    }


    stage('Build') {
      steps {
        sh 'env'
        // 还原 依赖
        sh 'nuget restore ${SLNNAME}.sln'
        sh 'dir'
        // 通过 MSBuild 编译，发布 Web
        // PublishProfile 文件路径要写绝对路径
          script {
            powershell """
            &"C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\MSBuild.exe"  ${SLNNAME}   /t:Rebuild /p:Configuration=Release  /p:DeployOnBuild=true /p:PublishProfile=${WORKSPACE}/${PUBXML}
            """
          }
      }
    }
  

  // 部署
    stage('Deploy') {
      steps {
        // 打包网站
        sh 'msdeploy.exe -verb:sync -source:iisApp=${PUBWEBDIR} -dest:package=${PUBWEBDIR}/web.zip'
        // 远程部署
        sh 'msdeploy.exe -verb:sync -source:package=${PUBWEBDIR}/web.zip -dest:iisApp="${IISAPP}",wmsvc=${IISADDR}/msdeploy.axd,username=${IISUSER},password=${IISADMIN},skipAppCreation=false -allowUntrusted=true'
      }
     }
     
     }
}
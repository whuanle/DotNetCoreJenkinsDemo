pipeline {

     // 全局环境变量
    environment {
        // 编译阶段的变量
        PROJDIR    =   'JenkinsDemo/src/WebDemo'                         // 项目路径
        PROJNAME   =   'JenkinsDemo/src/WebDemo/WebDemo.csproj'          // 要发布的主项目的 .csproj 文件路径

        // IIS 配置，远程发布的变量
        IISTMP     =   'C:/webdemo_tmp'                            // 用于打包发布的临时目录
        IISAPP     =   'jenkinsdemo.com'                           // 网站名称
        IISADDR    =   'https://192.168.0.66:8172/msdeploy.axd'    // WebDeploy 的地址
        IISUSER    =   'jenkinesdemo'                                    // 用于登录到 IIS 的账号密码
        IISADMIN   =   'jenkinesdemo'                
    }

    
    // 此流水线选择 windows 系统进行构建，如果你没有多节点，请注释
    agent {
        node {
            label 'windows'
    }
  }

    // 开始构建流水线
    stages {
        // 还原包
        stage('Build') { 
            steps {
                sh 'env'
                sh 'dotnet restore'
            }
        }

        // 执行单元测试
        stage('Test') { 
            steps {
                sh 'dotnet test  --logger "console;verbosity=detailed"  --blame  --logger trx'
            }
        }

        // 正式发布
        stage('Publish') { 
            steps {
                sh 'dotnet publish "${PROJNAME}" -c Release'
            }
        }

    // 打包部署
    stage('Deploy') {
      steps {

          script {
            powershell """
            rm -r ${IISTMP}
            """
          }

           script {
            powershell """
            mkdir ${IISTMP}
            mkdir ${IISTMP}/deploy
            """
          }

          script {
            powershell """
            cp "${WORKSPACE}/${PROJDIR}/bin/Release/netcoreapp3.1/publish/*" ${IISTMP}/deploy/ -r
            """
          }

        // 打包网站
          script {
            powershell """
                msdeploy.exe -verb:sync -source:iisApp="${IISTMP}/deploy" -dest:package="${IISTMP}/web.zip"
            """
          }

        // 远程部署文档
        // https://blog.richardszalay.com/2012/12/17/demystifying-msdeploy-skip-rules/
         script {
            powershell """
               msdeploy.exe -verb:sync -source:package="${IISTMP}/web.zip" -dest:iisApp=${IISAPP},wmsvc=${IISADDR},username=${IISUSER},password=${IISADMIN},skipAppCreation=false -allowUntrusted=true -skip:objectName="filePath",absolutePath="vue\$",skipAction=Delete
            """
          }
      }
     }

    }

}
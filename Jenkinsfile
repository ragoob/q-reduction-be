
@NonCPS
String getVersion(String branchName) {
  def matcher = (env.BRANCH_NAME =~ /([0-9]*\.[0-9]*)\.x/)
  if( matcher.matches() ){
	branchName = matcher[0][1]
  }
  return branchName
}

@NonCPS
String getBranchAndVersionTag(){
    def tag  = "${env.BRANCH_NAME}-b${env.BUILD_ID}"
    def matcher = (env.BRANCH_NAME =~ /([0-9]*\.[0-9]*)\.x/)
    if( matcher.matches() ){
        tag = "${matcher[0][1]}-b${env.BUILD_ID}"
    }
    return tag
}


 
pipeline {
  agent any
  options {
    timestamps()
  }
  
  environment {
    WORKSPACE = pwd()
    Rancher_deploy_url= "https://rancher.mohammedragab.com/v3/project/local:p-scq7m/workloads/deployment:myapps:q-reduction-api-qa?action=redeploy"
    
   
  }

  stages {
    
    stage('Build  image qa lastest') {
       when {
          expression { env.BRANCH_NAME == "Development" }
            }
      steps {
        script{
	        def devtest = docker.build("regoo707/q-reduction-api-qa:latest", './')
	        withDockerRegistry([ credentialsId: "registryCredential", url: "https://index.docker.io/v1/" ]) {
           devtest.push()
          }
          
            sh "docker rmi -f regoo707/q-reduction-api-qa:latest"
        }
      }
    } 

    stage('Build  image prod lastest') {
       when {
          expression { env.BRANCH_NAME == "master" || env.BRANCH_NAME == "main" }
            }
      steps {
        script{
	        def masterimage = docker.build("regoo707/q-reduction-api:latest", './')
	        withDockerRegistry([ credentialsId: "registryCredential", url: "https://index.docker.io/v1/" ]) {
           masterimage.push()
          }
          
            sh "docker rmi -f regoo707/q-reduction-api:latest"
        }
      }
    } 

      stage('Update Rancher Catalog and Upgrade App') {
	      when {
          expression { env.BRANCH_NAME == "Development" }
            }
      steps {
        script {
         
          withCredentials([string(credentialsId: 'rancher-token', variable: 'SECRET')]) {
                rancherApiToken = "${SECRET}"
		  sh "echo ${rancherApiToken}"
           }
             
            sh "curl -k --location --request POST '${Rancher_deploy_url}' --header 'Authorization: Bearer ${rancherApiToken}'"
        }
      }
    }

  }
  
}

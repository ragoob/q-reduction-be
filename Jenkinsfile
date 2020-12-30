
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
    
    stage('Build  image lastest') {
      steps {
        script{
	   def devtest = docker.build("regoo707/q-reduction-api-qa:latest", './')
	  withDockerRegistry([ credentialsId: "registryCredential", url: "" ]) {
           devtest.push()
          }
            devtest.push()
            sh "docker rmi -f regoo707/q-reduction-api-qa:latest"
        }
      }
    } 

      stage('Update Rancher Catalog and Upgrade App') {
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

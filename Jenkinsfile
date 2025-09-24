pipeline {
    agent any

    environment {
        DOTNET_HOME = 'C:\\Program Files\\dotnet'
        PATH = "${env.DOTNET_HOME};${env.PATH}"
    }

    stages {
        stage('Checkout SCM') {
            steps {
                checkout scm
            }
        }

        stage('Build .NET API') {
            steps {
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" build WebApplication2.sln -c Release'
            }
        }

        stage('Run Tests') {
            steps {
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger "trx;LogFileName=TestResults.trx" -l "console;verbosity=detailed"'
            }
        }

        stage('Convert TRX to JUnit') {
            steps {
                bat '"C:\\Users\\hp\\.dotnet\\tools\\trx2junit.exe" WebApplication2.Tests\\TestResults\\TestResults.trx'
            }
        }

        stage('Publish Test Results') {
            steps {
                junit 'WebApplication2.Tests\\TestResults\\TestResults.xml'
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Running code quality checks...'
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" tool restore --verbosity minimal'
                bat script: '"C:\\Program Files\\dotnet\\dotnet.exe" tool run dotnet-format WebApplication2.sln --check', returnStatus: true
            }
        }

        stage('Build Angular UI') {
            steps {
                script {
                    def NODEJS_HOME = tool name: 'NodeJS 20', type: 'NodeJS'
                    withEnv(["PATH+NODE=${NODEJS_HOME}\\bin"]) {
                        dir('C:\\Users\\hp\\source\\repos\\webapp-ui') {
                            bat 'npm install'
                            bat 'npm run build'
                        }
                    }
                }
            }
        }

        stage('Copy Angular UI to API') {
            steps {
                bat 'xcopy /E /Y "C:\\Users\\hp\\source\\repos\\webapp-ui\\dist\\webapp-ui" "WebApplication2\\wwwroot"'
            }
        }

        stage('Deploy') {
            steps {
                echo 'Add deployment steps here (e.g., IIS, Azure, Docker, etc.)'
            }
        }
    }

    post {
        always {
            echo 'Pipeline finished!'
        }
        success {
            echo 'Pipeline completed successfully!'
        }
        failure {
            echo 'Pipeline failed. Check logs for errors.'
        }
    }
}

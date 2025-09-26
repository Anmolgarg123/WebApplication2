pipeline {
    agent any

    environment {
        DOTNET_PATH = '"C:\\Program Files\\dotnet\\dotnet.exe"'
        PATH = "C:\\Program Files\\nodejs;C:\\Users\\hp\\.dotnet\\tools;${env.PATH}"
    }

    stages {
        stage('Checkout SCM') {
            steps {
                checkout scm
            }
        }

        stage('Debug Environment') {
            steps {
                echo "Debugging NodeJS and .NET environment..."
                bat 'node -v'
                bat 'npm -v'
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" --version'
            }
        }

        stage('Build .NET API') {
            steps {
                dir("${WORKSPACE}\\WebApplication2") {
                    bat "${DOTNET_PATH} build WebApplication2.sln -c Release"
                }
            }
        }

        stage('Run .NET Tests') {
            steps {
                dir("${WORKSPACE}\\WebApplication2") {
                    bat "${DOTNET_PATH} test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger \"trx;LogFileName=TestResults.trx\" -l \"console;verbosity=detailed\""
                }
            }
        }

        stage('Convert TRX to JUnit') {
            steps {
                dir("${WORKSPACE}\\WebApplication2") {
                    bat '"C:\\Users\\hp\\.dotnet\\tools\\trx2junit.exe" WebApplication2.Tests\\TestResults\\TestResults.trx'
                }
            }
        }

        stage('Publish Test Results') {
            steps {
                junit "${WORKSPACE}\\WebApplication2\\WebApplication2.Tests\\TestResults\\TestResults.xml"
            }
        }

        stage('Code Quality') {
            steps {
                dir("${WORKSPACE}\\WebApplication2") {
                    bat "${DOTNET_PATH} tool restore --verbosity minimal"
                    bat "${DOTNET_PATH} tool run dotnet-format WebApplication2.sln"
                }
            }
        }

        stage('Build Angular UI') {
            steps {
                dir("${WORKSPACE}\\webapp-ui") {
                    bat 'npm install'
                    bat 'npm run build --prod'
                }
            }
        }

        stage('Copy Angular UI to API') {
            steps {
                bat """
                robocopy "${WORKSPACE}\\webapp-ui\\dist\\webapp-ui" "${WORKSPACE}\\WebApplication2\\wwwroot" /E /NFL /NDL /NJH /NJS /nc /ns /np || exit 0
                """
            }
        }

        stage('Deploy') {
            steps {
                echo 'Add deployment steps here (IIS, Docker, or Azure)'
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

pipeline {
    agent any

    environment {
        DOTNET_PATH = "C:\\Program Files\\dotnet\\dotnet.exe"
        NODE_PATH = "C:\\Program Files\\nodejs"
        PATH = "${NODE_PATH};C:\\Users\\hp\\.dotnet\\tools;${env.PATH}"
    }

    options {
        timestamps()
        ansiColor('xterm')
    }

    stages {
        stage('Checkout SCM') {
            steps {
                checkout scm
            }
        }

        stage('Debug Environment') {
            steps {
                echo "Node & .NET versions"
                bat 'node -v'
                bat 'npm -v'
                bat '"C:\\Program Files\\dotnet\\dotnet.exe" --version'
            }
        }

        stage('Build .NET API') {
            steps {
                bat "${DOTNET_PATH} build WebApplication2.sln -c Release"
            }
        }

        stage('Run .NET Tests') {
            steps {
                bat "${DOTNET_PATH} test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger \"trx;LogFileName=TestResults.trx\" -l \"console;verbosity=detailed\""
            }
        }

        stage('Convert TRX to JUnit') {
            steps {
                script {
                    def trxFile = 'WebApplication2.Tests\\TestResults\\TestResults.trx'
                    if (fileExists(trxFile)) {
                        bat "\"C:\\Users\\hp\\.dotnet\\tools\\trx2junit.exe\" ${trxFile}"
                    } else {
                        echo "TRX file not found, skipping conversion."
                    }
                }
            }
        }

        stage('Publish Test Results') {
            steps {
                junit 'WebApplication2.Tests\\TestResults\\TestResults.xml'
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Running dotnet-format...'
                bat "${DOTNET_PATH} tool restore --verbosity minimal"
                bat "${DOTNET_PATH} tool run dotnet-format WebApplication2.sln || exit 0"
                // Optional: Add SonarQube analysis for HD
            }
        }

        stage('Build Angular UI') {
            steps {
                dir('C:\\Users\\hp\\source\\repos\\webapp-ui') {
                    bat 'rmdir /s /q dist || exit 0'
                    bat 'npm install'
                    bat 'npm run build -- --prod'
                }
            }
        }

        stage('Copy Angular UI to API') {
            steps {
                bat '''
                robocopy "C:\\Users\\hp\\source\\repos\\webapp-ui\\dist\\webapp-ui" "WebApplication2\\wwwroot" /E /NFL /NDL /NJH /NJS /nc /ns /np || exit 0
                '''
            }
        }

        stage('Deploy') {
            steps {
                echo 'Deploying to IIS / Docker / Azure... (add your deployment steps here)'
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
            echo 'Pipeline failed! Check logs.'
        }
    }
}

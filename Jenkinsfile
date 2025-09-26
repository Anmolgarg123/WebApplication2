pipeline {
    agent any

    environment {
        DOTNET_PATH = '"C:\\Program Files\\dotnet\\dotnet.exe"'
        NODE_PATH = "C:\\Program Files\\nodejs"
        PATH = "${NODE_PATH};C:\\Users\\hp\\.dotnet\\tools;${env.PATH}"
    }

    stages {
        stage('Checkout SCM') {
            steps {
                echo "Checking out source code..."
                checkout scm
            }
        }

        stage('Debug Environment') {
            steps {
                echo "Node.js and .NET versions:"
                bat 'node -v'
                bat 'npm -v'
                bat "${DOTNET_PATH} --version"
            }
        }

        stage('Build .NET API') {
            steps {
                echo "Building .NET API..."
                bat "${DOTNET_PATH} build WebApplication2.sln -c Release"
            }
        }

        stage('Run API Tests') {
            steps {
                echo "Running API tests..."
                bat "${DOTNET_PATH} test WebApplication2.Tests\\WebApplication2.Tests.csproj --logger \"trx;LogFileName=TestResults.trx\" -l \"console;verbosity=detailed\""
            }
        }

        stage('Convert Test Results') {
            steps {
                echo "Converting TRX to JUnit..."
                bat '"C:\\Users\\hp\\.dotnet\\tools\\trx2junit.exe" WebApplication2.Tests\\TestResults\\TestResults.trx'
            }
        }

        stage('Publish Test Results') {
            steps {
                echo "Publishing test results..."
                junit 'WebApplication2.Tests\\TestResults\\TestResults.xml'
            }
        }

        stage('Code Quality') {
            steps {
                echo "Running code quality checks..."
                bat "${DOTNET_PATH} tool restore --verbosity minimal"
                bat "${DOTNET_PATH} tool run dotnet-format WebApplication2.sln"
            }
        }

        stage('Build Angular UI') {
            steps {
                echo "Building Angular UI..."
                dir('C:\\Users\\hp\\source\\repos\\webapp-ui') {
                    bat 'npm install'
                    bat 'npm run build'
                }
            }
        }

        stage('Copy Angular UI to API wwwroot') {
            steps {
                echo "Copying Angular build to API wwwroot..."
                bat '''
                robocopy "C:\\Users\\hp\\source\\repos\\webapp-ui\\dist\\webapp-ui" "WebApplication2\\wwwroot" /E /NFL /NDL /NJH /NJS /nc /ns /np || exit 0
                '''
            }
        }

        stage('Deploy (Manual/Placeholder)') {
            steps {
                echo "Deploy step: Add your deployment here (IIS, Docker, Azure, etc.)"
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
            echo 'Pipeline failed. Check logs!'
        }
    }
}

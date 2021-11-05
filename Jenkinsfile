pipeline {
    agent any

    stages {
        stage ("Build") {
            steps
            {
                echo "Starting Build stage"
                sh "dotnet build"
            }
            post {
                failure {
                    echo "Build failed"
                }
                success {
                    echo "Build succeded"
                }
            }
        }

        stage ("Test") {
            steps {
                echo "Starting Test stage"
                sh "dotnet test"
            }

            post {
                failure {
                    echo "Test failed"
                }
                success {
                    echo "Test succeded"
                }
            }
        }

    }
}
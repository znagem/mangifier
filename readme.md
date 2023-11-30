# e-Mango

A Philippine Super Mango Quality and Disease Identification and Prevention using Convolutional Neural Networks Algorithm

## System Flow Chart of Proposed Methodology

```mermaid
flowchart

user("User")
admin("Admin")
tensorflow("Tensorflow")
train["Train Model"]
trained_models("Trained Models")
service_analyzer("Service Analyzer")
web_api("Web API Server")
analyze["Analyze Image"]
algorithm{{"CNN Algorithm"}}
capture["Capture Image"]
mobile_app("Mobile App")

user --> mobile_app
mobile_app --> capture
capture --> web_api
web_api --> service_analyzer
service_analyzer --> analyze
analyze --> algorithm
analyze --> service_analyzer
service_analyzer --> web_api
web_api --> mobile_app

admin --> tensorflow
tensorflow --> train
train --> trained_models
trained_models --> service_analyzer
```

## Technologies Stack

1. [Flutter](https://flutter.dev/) for mobile development
2. [Tensorflow](https://www.tensorflow.org/) for machine learning
3. [.NET](https://dotnet.microsoft.com/en-us/) for web api and service analyzer

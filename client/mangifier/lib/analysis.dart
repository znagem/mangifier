import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:mangifier/analyzing.dart';
import 'package:mangifier/failed.dart';
import 'package:mangifier/models/diagnosis.dart';
import 'package:mangifier/result.dart';
import 'package:mangifier/services/analyzer_service.dart';

class Analysis extends StatefulWidget {
  const Analysis({super.key, required this.imagePath});

  final String imagePath;

  @override
  State<Analysis> createState() => _AnalysisState();
}

class _AnalysisState extends State<Analysis> {
  var _result = Diagnosis(diseases: []);

  final client = http.Client();

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<void>(
      future: _analyze(client),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.done) {
          if (_result.diseases.isEmpty) {
            return Failed(
              message: "Unable to analyze image.",
              onRetry: () => setState(() {}),
            );
          }
          return Result(
            imagePath: widget.imagePath,
            result: _result,
          );
        } else {
          return PopScope(
            child: Analyzing(
              imagePath: widget.imagePath,
              client: client,
            ),
          );
        }
      },
    );
  }

  Future<void> _analyze(http.Client client) async {
    _result = await AnalyzerService().analyze(widget.imagePath, client);
  }
}

import 'dart:io';

import 'package:flutter/material.dart';
import 'package:mangifier/base.dart';
import 'package:mangifier/models/diagnosis.dart';

class Result extends StatefulWidget {
  const Result({super.key, required this.imagePath, required this.result});

  final Diagnosis result;
  final String imagePath;

  @override
  State<Result> createState() => _ResultState();
}

class _ResultState extends State<Result> {
  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return Base(
      appBar: AppBar(
        leading: IconButton(
          onPressed: () => {Navigator.pop(context)},
          icon: const Icon(Icons.chevron_left, color: Colors.white),
        ),
        title: const Text("Result"),
      ),
      child: SingleChildScrollView(
        child: Center(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              SizedBox(
                width: 250,
                height: 250,
                child: ClipOval(
                  child: Transform.scale(
                      scale: 1 / 0.7,
                      child: Image.file(File(widget.imagePath),
                          fit: BoxFit.cover)),
                ),
              ),
              Column(children: _buildDiagnosisItems(widget.result, theme)),
            ],
          ),
        ),
      ),
    );
  }

  List<Widget> _buildDiagnosisItems(Diagnosis diagnosis, ThemeData theme) {
    return diagnosis.diseases
        .map(
          (d) => Column(
            children: [
              Container(
                margin: const EdgeInsets.all(10),
                child: Card(
                  child: Column(
                    children: [
                      Container(
                        margin: const EdgeInsets.fromLTRB(20, 10, 20, 10),
                        child: Align(
                          alignment: Alignment.topLeft,
                          child: Wrap(
                            crossAxisAlignment: WrapCrossAlignment.center,
                            children: [
                              Text(
                                '${(d.score * 100).toStringAsFixed(2)}%',
                                style: theme.textTheme.displaySmall,
                              ),
                              const SizedBox(width: 10),
                              Flexible(
                                child: Text(
                                  d.name,
                                  style: theme.textTheme.headlineMedium,
                                ),
                              ),
                            ],
                          ),
                        ),
                      ),
                      Container(
                        margin: const EdgeInsets.fromLTRB(20, 0, 20, 10),
                        child: Column(
                          children: [
                            Align(
                              alignment: Alignment.centerLeft,
                              child: Text(
                                "Symptoms",
                                style: theme.textTheme.titleMedium,
                              ),
                            ),
                            Column(
                              children: _buildSubItems(d.symptoms, theme),
                            ),
                          ],
                        ),
                      ),
                    ],
                  ),
                ),
              ),
              Container(
                margin: const EdgeInsets.all(10),
                child: Card(
                  child: Column(
                    children: [
                      Container(
                        margin: const EdgeInsets.fromLTRB(20, 10, 20, 10),
                        child: Column(
                          children: [
                            Align(
                              alignment: Alignment.centerLeft,
                              child: Text(
                                "Preventions",
                                style: theme.textTheme.headlineSmall,
                              ),
                            ),
                            Column(
                              children: _buildSubItems(d.preventions, theme),
                            ),
                          ],
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
        )
        .toList();
  }

  List<Widget> _buildSubItems(List<String> symptoms, ThemeData theme) {
    return symptoms
        .map(
          (symptom) => Container(
            margin: const EdgeInsets.only(top: 5, bottom: 5),
            child: Text(
              symptom,
              style: theme.textTheme.bodyMedium,
            ),
          ),
        )
        .toList();
  }
}

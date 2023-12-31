import 'package:flutter/material.dart';
import 'package:mangifier/base.dart';

class InfoPage extends StatelessWidget {
  const InfoPage({super.key});

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    return Base(
      appBar: AppBar(
        title: const Text("App Info"),
        centerTitle: true,
        leading: IconButton(
          onPressed: () => Navigator.pop(context),
          icon: const Icon(
            Icons.chevron_left,
            color: Colors.white,
          ),
        ),
      ),
      child: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(10.0),
          child: Center(
            child: Text(
              "This is app info",
              style: theme.textTheme.labelLarge,
            ),
          ),
        ),
      ),
    );
  }
}

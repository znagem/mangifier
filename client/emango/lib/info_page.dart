import 'package:flutter/material.dart';

class InfoPage extends StatelessWidget {
  const InfoPage({super.key, required this.title});

  final String title;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.transparent,
        leading: IconButton(
          onPressed: () => {Navigator.pop(context)},
          icon: const Icon(Icons.chevron_left),
        ),
        title: Text(title),
      ),
      body: const Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.start,
          children: [
            Text('This is where the app info will go'),
          ],
        ),
      ),
    );
  }
}

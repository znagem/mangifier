import 'package:flutter/material.dart';
import 'home_page.dart';

void main() {
  runApp(const MainPage());
}

class MainPage extends StatelessWidget {
  const MainPage({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Mangifier',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: const Color(0xFF09474E)),
        useMaterial3: true,
      ),
      home: const HomePage(title: 'Mangifier'),
    );
  }
}

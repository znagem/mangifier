import 'package:flutter/material.dart';
import 'package:flutter_animation_progress_bar/flutter_animation_progress_bar.dart';
import 'package:mangifier/info_page.dart';
import 'package:mangifier/settings_page.dart';

class HomePage extends StatefulWidget {
  const HomePage({super.key, required this.title});

  final String title;

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        // Add box decoration
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Image.asset(
              'assets/images/logo.png',
              width: 200,
            ),
            Container(
              alignment: Alignment.center,
              margin: const EdgeInsets.fromLTRB(75, 0, 75, 0),
              child: FAProgressBar(
                currentValue: 50,
                displayText: '%',
                borderRadius: BorderRadius.circular(8),
                progressColor: const Color(0xfff09835),
                backgroundColor: const Color(0xFFFADCBB),
              ),
            )
          ],
        ),
      ),
      bottomNavigationBar: BottomAppBar(
          shape: const CircularNotchedRectangle(),
          child:
              Row(mainAxisAlignment: MainAxisAlignment.spaceEvenly, children: [
            IconButton(
              tooltip: 'App info',
              iconSize: 32,
              icon: const Icon(Icons.info_outline_rounded),
              onPressed: () => {
                Navigator.push(
                    context,
                    MaterialPageRoute(
                        builder: (context) =>
                            const InfoPage(title: 'App Info')))
              },
            ),
            const SizedBox(width: 48),
            IconButton(
              tooltip: 'Settings',
              iconSize: 32,
              icon: const Icon(Icons.settings_outlined),
              onPressed: () => {
                Navigator.push(
                    context,
                    MaterialPageRoute(
                        builder: (context) =>
                            const SettingsPage(title: 'Settings')))
              },
            ),
          ])),
      floatingActionButton: FloatingActionButton(
        onPressed: () => setState(() {}),
        tooltip: 'Capture image',
        backgroundColor: const Color(0xff09474E),
        foregroundColor: Colors.white,
        child: const Icon(Icons.camera, size: 28),
      ),
      floatingActionButtonLocation: FloatingActionButtonLocation.centerDocked,
    );
  }
}

import 'dart:async';

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class ToastContext {
  BuildContext? context;
  MethodChannel? _channel;

  static final ToastContext _instance = ToastContext._internal();

  factory ToastContext() {
    return _instance;
  }

  ToastContext init(BuildContext context) {
    _instance.context = context;
    return _instance;
  }

  ToastContext._internal();
}

class Toast {
  static const int lengthShort = 1;
  static const int lengthLong = 3;
  static const int bottom = 0;
  static const int center = 1;
  static const int top = 2;

  static void show(String msg,
      {int? duration = 1,
      ToastPosition position = ToastPosition.bottom,
      Color backgroundColor = const Color(0xAA000000),
      textStyle = const TextStyle(fontSize: 14, color: Colors.white),
      double backgroundRadius = 8,
      bool? rootNavigator,
      Border? border,
      bool webShowClose = false,
      Color webTexColor = const Color(0XFFFFFFFF)}) {
    if (ToastContext().context == null) {
      throw Exception(
          'Context is null, please call ToastContext.init(context) first');
    }
    if (kIsWeb == true) {
      if (ToastContext()._channel == null) {
        ToastContext()._channel = const MethodChannel('appdev/FlutterToast');
      }

      final positions = {
        ToastPosition.top: "top",
        ToastPosition.bottom: "bottom",
        ToastPosition.center: "center",
      };

      final Map<String, dynamic> params = <String, dynamic>{
        'msg': msg,
        'duration': (duration ?? 1) * 1000,
        'position': positions[position] ?? "bottom",
        'bgcolor': backgroundColor.toString(),
        'textcolor': webTexColor.value.toRadixString(16),
        'webShowClose': webShowClose,
      };
      ToastContext()._channel?.invokeMethod("showToast", params);
    } else {
      ToastView.dismiss();
      ToastView.createView(msg, ToastContext().context!, duration, position,
          backgroundColor, textStyle, backgroundRadius, border, rootNavigator);
    }
  }

  static void showSuccess(String msg) {
    show(msg, backgroundColor: const Color(0xFF1CA345));
  }

  static void showError(String msg) {
    show(msg, backgroundColor: const Color(0xFFDE3E44));
  }
}

enum ToastPosition { top, center, bottom }

class ToastView {
  static final ToastView _singleton = ToastView._internal();

  factory ToastView() {
    return _singleton;
  }

  ToastView._internal();

  static OverlayState? overlayState;
  static OverlayEntry? _overlayEntry;
  static bool _isVisible = false;
  static Timer? _timer;

  static void createView(
      String msg,
      BuildContext context,
      int? duration,
      ToastPosition position,
      Color background,
      TextStyle textStyle,
      double backgroundRadius,
      Border? border,
      bool? rootNavigator) async {
    overlayState = Overlay.of(context, rootOverlay: rootNavigator ?? false);

    _overlayEntry = OverlayEntry(
      builder: (BuildContext context) => ToastWidget(
          widget: Container(
            alignment: Alignment.center,
            width: MediaQuery.of(context).size.width,
            child: Container(
              decoration: BoxDecoration(
                color: background,
                borderRadius: BorderRadius.circular(backgroundRadius),
                border: border,
              ),
              margin: const EdgeInsets.symmetric(horizontal: 10),
              padding: const EdgeInsets.fromLTRB(16, 10, 16, 10),
              child: Text(msg, softWrap: true, style: textStyle),
            ),
          ),
          position: position),
    );

    _isVisible = true;
    overlayState!.insert(_overlayEntry!);

    _timer = Timer(
      Duration(seconds: duration ?? Toast.lengthShort),
      () => dismiss(),
    );
  }

  static dismiss() {
    _timer?.cancel();

    if (_isVisible) {
      _isVisible = false;
      _overlayEntry?.remove();
    }
  }
}

class ToastWidget extends StatelessWidget {
  const ToastWidget({
    super.key,
    required this.widget,
    required this.position,
  });

  final Widget widget;
  final ToastPosition position;

  @override
  Widget build(BuildContext context) {
    return Positioned(
      top: position == ToastPosition.top
          ? MediaQuery.of(context).viewInsets.top + 50
          : null,
      bottom: position == ToastPosition.bottom
          ? MediaQuery.of(context).viewInsets.bottom + 50
          : null,
      child: IgnorePointer(
        child: Material(
          color: Colors.transparent,
          child: widget,
        ),
      ),
    );
  }
}

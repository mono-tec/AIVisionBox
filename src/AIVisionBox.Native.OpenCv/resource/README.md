# AIVisionBox.Native.OpenCv

AIVisionBox における **Native OpenCV 画像処理エンジン** を実装するプロジェクトです。  
C++ による高速な画像処理を提供し、C# / Web / PLC など上位層から共通に利用されることを目的としています。

---

## 役割と位置づけ

本プロジェクトは、以下の責務を持ちます。

- OpenCV を用いた画像処理ロジックの実装
- C API（DLL export）として機能を提供
- OS や UI に依存しない画像処理基盤の提供

> 本プロジェクトは **UI や AI 推論そのものを担当しません**。  
> あくまで「画像処理エンジン」としての役割に限定しています。

---

## なぜ Native（C++）なのか

- OpenCV は C++ が本来の実装言語
- Python は検証用途として有用だが、商用・組み込みでは管理コストが高い
- Renesas RZ/V 等の AI SoC では C/C++ が前提となるケースが多い

そのため、

- **画像処理は C++**
- **制御・UI・通信は C# / Web**

という分業構成を採用しています。

---

## 提供インターフェース（C API）

本プロジェクトは C API 形式で関数を公開します。

例：

```cpp
AivbCountResult Aivb_CountDiamonds_Bgr24(
    const unsigned char* bgr,
    int width,
    int height,
    int strideBytes,
    AivbRoiRect roi,
    int minArea,
    int maxArea
);
```
- 言語間 ABI を安定させるため extern "C" を使用（C++の名前修飾を回避）
- C# P/Invoke / 他言語バインディングを容易にする設計

---

## ディレクトリ構成
```text
AIVisionBox.Native.OpenCv/
├─ include/        // 外部公開用ヘッダ（C API）
│  ├─ AivbOpenCvNative.h
│  ├─ AivbTypes.h
│  └─ OpenCvCardDiamondCounter.h
├─ src/            // 実装
│  ├─ AivbOpenCvNative.cpp
│  ├─ OpenCvCardDiamondCounter.cpp
│  └─ dllmain.cpp

```

- include/ は 他プロジェクトから参照される前提
- src/ は内部実装用

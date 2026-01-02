# AIVisionBox

AIVisionBox は、**AI × 画像処理 × 制御システム**を前提とした  
**拡張可能な推論基盤（アプリケーションフレームワーク）**です。

本リポジトリは、

- 実機 AI チップが無い状態でも開発を進められる
- 推論エンジンを差し替え可能な構成を持つ
- 産業用途（FA / 制御 / 画像検査）を想定した設計

を目的として構築されています。

---

## コンセプト

AIVisionBox は「AIモデルを作る」ことを目的としていません。

代わりに、

- **既存の AI 推論技術**
- **画像処理（OpenCV 等）**
- **PLC・制御・通信技術**

を **現場で使える形に統合する基盤**を提供します。

> AI は“作るもの”ではなく、“組み込むもの”

という立ち位置を明確にしています。

---

## 全体構成

```
AIVisionBox
├─ AIVisionBox.Core
├─ AIVisionBox.Engine.HttpClient
├─ AIVisionBox.Engine.OpenCv       （予定）
├─ AIVisionBox.Mock.InferenceServer
├─ AIVisionBox.Mock.InferenceServer.Tests
├─ AIVisionBox.App.ConsoleApp
└─ docs
```

---

## 現在のステータス

- Core（契約モデル）：安定
- HttpInferenceEngine：実装済み
- Mock Inference Server：実装済み
- UnitTest：Core / Engine / Server
- OpenCV Engine（C++/CLI）：計画中
- Blazor UI：計画中

---

## 特徴

- 推論エンジン差し替え設計
- 実機なしでの先行開発
- 産業用途を前提とした堅牢設計

---

## クイックスタート（Mock 推論）

1. Mock サーバ起動  
   ```
   dotnet run --project AIVisionBox.Mock.InferenceServer
   ```

2. Console アプリ起動  
   ```
   dotnet run --project AIVisionBox.App.ConsoleApp
   ```

---

## サンプルデータ（cards.png）の取得と配置

OpenCV 公式サンプル画像 `cards.png` を使用します。

- 取得箇所：`opencv` リポジトリ内のサンプルデータ  
  - https://github.com/opencv/opencv/blob/4.x/samples/data/cards.png
- 配置場所：本リポジトリの以下へ配置してください  
  - `（リポジトリルート）\assets\opencv\cards.png`

> ※ `AIVisionBox.Native.OpenCv.SmokeTest` およびテストで参照します。

---

## 環境変数（開発環境セットアップ）

本リポジトリでは、OpenCV の配置場所とサンプルデータ（画像）配置場所を環境変数で切り替えられる想定です。

### OPENCV_DIR

- 内容：ダウンロードした OpenCV の配置場所（ルートパス）
- 想定構成（例）：

```
OPENCV_DIR
├─ build
├─ sources
├─ LICENSE.txt
├─ LICENSE_FFMPEG.txt
└─ README.md.txt
```

> ※ OpenCV の入手方法やフォルダ名は環境により異なりますが、  
> `build`（ビルド成果物）と `sources`（ソース）を同一ルート配下に置く形を想定しています。

### AIVB_ASSETS_DIR

- 内容：サンプルデータ（画像）の置き場（assets ルート）
- 例：`（リポジトリルート）\assets`

> 例：`AIVB_ASSETS_DIR\opencv\cards.png` が存在する状態にします。

---

## 未完部分（v0.1.x の扱い）

### cards.png のダイヤ個数カウント精度

現状の OpenCV 実装は「HSV 赤抽出 + 形態学処理 + 輪郭面積フィルタ」による **簡易カウント**です。

- v0.1.x の目的：  
  **Native API 契約（DLL export / 呼び出し）・ビルド/配布・再現可能なパイプライン**の安定化
- 精度（正解個数の一致）は v0.2 以降で調整予定です  
  - HSV 閾値 / morphology / minArea/maxArea は `cards.png` と環境差により変動します

## ライセンス
MIT © 2026 mono-tec

---

## 注意事項

本プロジェクトは学習・検証用途を主目的としています。

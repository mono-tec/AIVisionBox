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

## ライセンス

MIT © 2026 mono-tec

---

## 注意事項

本プロジェクトは学習・検証用途を主目的としています。


---

## Notebook（Python での検証・チューニング）

本リポジトリには、OpenCV の処理パイプラインを **Python / Jupyter Notebook で先に検証し**、  
確度が取れた内容を C++ 実装へフィードバックするための Notebook を同梱します。

- `notebooks/cards_diamond_practice.ipynb`  
  - 画像の読み込み、BGR→RGB、ROI の考え方などの練習用
- `notebooks/cards_diamond_tuning.ipynb`  
  - HSV 赤抽出 → 形態学処理 → 輪郭 → 面積フィルタ（min/max area）までの一連のパイプラインを可視化し、
    パラメータを調整するための Notebook

> 方針：**Pythonで最適化 → C++へ反映**  
> まず「見える化＋パラメータ探索」を Python で行い、C++ は最小で堅牢な実装に寄せます。

### Notebook の実行前提

- Python 3.10+（推奨）
- `opencv-python`, `numpy`, `matplotlib`, `requests`（Notebook 内でインストール案内あり）

---

## サンプル画像 cards.png の扱い（リポジトリに含めない方針）

`cards.png` は OpenCV 公式リポジトリのサンプルデータです。

- 取得箇所：OpenCV リポジトリ内のサンプル画像  
  - https://github.com/opencv/opencv/blob/4.x/samples/data/cards.png

本リポジトリでは **cards.png を原則コミットしません**（著作権/ライセンス確認を簡単にするため）。  
代わりに次のどちらかで利用します。

1. **ローカルに配置して使う（C++ SmokeTest / Native テスト向け）**  
   - 配置：`（リポジトリルート）\assets\opencv\cards.png`
2. **Notebook が必要に応じてダウンロードして使う（Python 検証向け）**  
   - Notebook 内の `ensure_cards_png()` がダウンロードを行います

> ※ どちらの方式でも `AIVB_ASSETS_DIR` を設定して assets の基準パスを切り替え可能です。

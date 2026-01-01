# AIVisionBox.Mock.InferenceServer

AIVisionBox における **推論サーバのモック実装**です。

本プロジェクトは、実際の AI チップや推論 SDK（例：Renesas RZ/V シリーズ）が
手元にない状態でも、

- クライアント側（Engine / App）の開発
- HTTP 通信仕様の確定
- 推論結果のデータ契約検証

を行うための **擬似推論サーバ**として機能します。

---

## 目的

- AI 推論部分を **インターフェースとして分離**する
- 実機や SDK に依存せずにアプリケーション開発を進める
- HTTP ベースの推論連携を事前に検証する

---

## 提供する API

### `GET /`
サーバ稼働確認用エンドポイント。

**レスポンス例**
```text
AIVisionBox Mock Inference Server is running.
```

---

### `POST /infer`
推論実行用エンドポイント（モック）。

**リクエスト例**
```json
{
  "model": "MockModel",
  "image": {
    "format": "GRAY8",
    "width": 1,
    "height": 1,
    "dataBase64": "AA=="
  }
}
```

**レスポンス例**
```json
{
  "isOk": true,
  "objectCount": 5,
  "message": "mock inference result",
  "engine": "MockServer",
  "model": "MockModel",
  "detections": [
    {
      "label": "screw",
      "score": 0.92,
      "x": 10,
      "y": 20,
      "w": 30,
      "h": 40
    }
  ]
}
```

---

## 実装方針

- ASP.NET Core Minimal API を使用
- 実際の推論処理は行わない
- 推論結果は **固定値または簡易ロジック**で返却
- JSON 構造は、将来の実 AI 推論サーバと同一形式を想定

---

## 想定ユースケース

- `AIVisionBox.Engine.HttpClient` の開発・テスト
- Console / Web UI からの推論フロー検証
- CI 上での自動テスト用サーバ

---

## 将来拡張（想定）

- OpenCV を用いた簡易画像処理の追加
- 推論結果を入力画像に応じて変化させる
- 実 AI 推論サーバ（Renesas RZ/V SDK 等）への差し替え

---

## 注意事項

本プロジェクトは **開発・検証用途のモック**です。  
商用・実運用での使用は想定していません。

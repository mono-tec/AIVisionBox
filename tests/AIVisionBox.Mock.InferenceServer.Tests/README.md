# AIVisionBox.Mock.InferenceServer.Tests

`AIVisionBox.Mock.InferenceServer` に対する  
**インメモリ統合テスト（Integration Test）**を提供します。

---

## 目的

- Mock 推論サーバの API 契約が壊れていないことを保証する
- `/infer` の HTTP メソッド制約（POST 専用）を検証する
- 推論レスポンスの JSON 構造を自動的に検証する

---

## テスト方針

- `WebApplicationFactory<Program>` を使用
- 実際に Kestrel を起動せず **インメモリで API をホスト**
- `HttpClient` 経由で API を呼び出す
- ポート競合や証明書問題を回避

---

## テスト対象

### 1. サーバ起動確認
```http
GET /
```

- サーバが起動していること
- 稼働確認メッセージが返ること

---

### 2. 推論 API（POST）
```http
POST /infer
```

- 正常レスポンス（200 OK）
- 推論結果 JSON が返ること
- 必須フィールドが含まれていること

---

### 3. 非対応メソッドの検証
```http
GET /infer
```

- `405 Method Not Allowed` が返ること

---

## 採用している理由

- Mock サーバは **仕様の基準点**になるため
- 実 AI 推論サーバへ差し替える際の影響範囲を明確にするため
- CI / GitHub Actions 上でも安定して実行できるため

---

## 将来拡張（想定）

- エラーレスポンス（500 / 400）のテスト追加
- 入力画像サイズ・形式に応じた挙動テスト
- OpenCV ベースの簡易処理を含めたテスト

---

## 注意事項

本テストは **モックサーバ専用**です。  
実 AI 推論サーバ用のテストは別プロジェクトで実装します。

# SelectAid

SelectAid は AAC（意思伝達装置）と PC 操作支援（オーバーレイ＋マウスグリッド）を統合した .NET 8 / WPF アプリです。視線入力・スイッチ・ジャイロ入力などの入力方式を共通の入力抽象で扱い、誤操作時に戻れる運用性を重視しています。

## ビルド

```bash
# .NET 8 SDK (Windows 10/11)
dotnet build SelectAid.sln
```

## 起動

```
SelectAid/bin/Debug/net8.0-windows/SelectAid.exe
```

## 基本操作（全画面共通）

- **Home**: どこからでもホームへ戻ります。
- **Back**: 現在画面からホームへ戻ります。
- **Undo**: AAC の履歴を取り消します。
- **Pause**: 入力を停止/再開します（EmergencyStop を除く）。
- **Buzzer**: 即時ブザー（音＋画面フラッシュ）。
- **PC**: PC 操作支援（Overlay/MouseGrid）画面を開きます。

## AAC（意思伝達）

1. **AAC** 画面で文字盤を選び、入力してください。
2. **Speak** を押すと SAPI で発話し履歴に記録します。
3. **Predictions** の候補は履歴頻度＋ユーザー辞書から生成されます（最大8件）。
4. **Phrases** 画面で定型文を追加すると即座に反映されます。

## 定型文と文字盤

- **Phrases**: Speak（即発話）/ Insert（編集欄へ投入）を選べます。
- **Keyboard Layouts**: レイアウトの有効/無効を切り替えできます。

## PC 操作支援（Overlay / MouseGrid）

- **Overlay Toggle**: 常時最前面の操作パネルを表示/非表示。
- **Overlay** 内のボタンは SendInput で OS にクリック/スクロール/Tab/Backspace を送出します。
- **Mouse Grid**: 2〜6分割のグリッドで範囲を絞り込み、最後にクリックします。
- **Back**: 1段戻り（拡大解除）。

## スキャン（SwitchScan）

- **L1/L2/L3** の3階層スキャンで領域→グループ→項目の順に巡回します。
- SwitchScan モード時は自動スキャンが走り、Confirm/Cancel によって階層移動します。

## テーマ切替 / 高コントラスト

Supporter 画面から Friendly / Stylish / Kids を切り替えできます。高コントラストは Settings から ON/OFF 可能で、再起動不要です。

## 支援者設定

- **Input Mode**: Eye / Eye+Switch / Eye+Gyro / Gyro / SwitchScan
- **Auto Start**: Windows 起動時の自動起動を ON/OFF
- **Power Permissions**: Shutdown/Restart/Sleep/Logoff の許可制御
- **Power Actions**: 許可済みの場合のみ長押し確定で実行

## バックアップと復元

- **Backup/Restore** 画面で "Create Backup" を押すと `%AppData%\SelectAid\backups` に zip を作成します。
- **Restore Selected** は長押しで確定します。復元前に自動で退避バックアップが作成されます。

## ログ

`%AppData%\SelectAid\log.txt` にエラーログを出力します。

## セーフ起動

起動時に Shift キーを押し続けると SafeMode を記録し、支援者側で復旧操作を行えるようにします。

## 復旧手順（EmergencyStop / セーフ起動 / バックアップ）

1. **EmergencyStop**（F12 などに割当）で必ず Home へ戻れる設計。
2. 起動時に **Shift 長押し**で SafeMode を記録し、Supporter 画面から復旧操作。
3. **Backup/Restore** でバックアップ zip を作成し、異常時は復元。

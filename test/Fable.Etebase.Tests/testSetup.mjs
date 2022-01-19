import { Account } from "etebase";
import { createRequire } from "module";
const require = createRequire(import.meta.url);
const testData  = require("../../dist/tests/TestHelpers").testData;

async function check(username, email, password) {
  try {
    await Account.login(username, password, testData.Server);
  } catch {
    await Account.signup({ username, email }, password, testData.Server);
  }
}

await check(
  testData.User1.Username,
  testData.User1.Email,
  testData.User1.Password
);
await check(
  testData.User2.Username,
  testData.User2.Email,
  testData.User2.Password
);


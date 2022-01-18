import { Account } from "etebase";

const username = "JessicaHyde";
const password = "Mr. Rabbit";
const email = "jessicahyde@example.com";
const server = "http://172.18.115.224:3735";

async function check() {
  try {
    await Account.login(username, password, server);
  } catch {
    await Account.signup({ username, email }, password, server);
  }
}

await check();

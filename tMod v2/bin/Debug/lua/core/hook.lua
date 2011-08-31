local print = print
local table = table
local pairs = pairs
local pcall = pcall
local ErrorNoHalt = ErrorNoHalt

module("hook")
hooks = {}

function Call(HookName, ...)
	local Hook = hooks[HookName]
	if not Hook then
		return
	end
	
	for HookUnique, HookFunction in pairs(Hook) do
		local call, error = pcall(HookFunction, ...)
		if not call then
			print("[Lua] Error with hook " .. HookUnique)
			print("[Lua] " .. error)
			Remove(HookName, HookUnique)
		end
	end
end

function Add(HookName, HookUnique, HookFunction)
	hooks[HookName] = hooks[HookName] or {}
	hooks[HookName][HookUnique] = HookFunction
end

function Remove(HookName, HookUnique)
	if not hooks[HookName] then
		return
	end
	hooks[HookName][HookUnique] = nil
end
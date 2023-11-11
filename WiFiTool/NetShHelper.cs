using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WiFiTool
{
    public static class NetShHelper
    {
        public static async Task<List<string>> GetWiFiProfilesAsync()
        {
            var names = new List<string>();

            var psi = new ProcessStartInfo()
            {
                FileName = "netsh",
                Arguments = "wlan show profiles",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var proc = Process.Start(psi);

            await proc.WaitForExitAsync();
            if (proc.ExitCode != 0)
            {
                Debug.WriteLine($"Error in GetWiFiProfilesAsync(). ExitCode={proc.ExitCode}");
                return names;
            }

            string output = await proc.StandardOutput.ReadToEndAsync();
            string pattern = @"All User Profile\s+:\s+(?<ProfileName>\w+)";
            var regex = new Regex(pattern);
            var matches = regex.Matches(output);

            foreach (Match match in matches)
            {
                string name = match.Groups["ProfileName"].Value;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    names.Add(name);
                }
            }

            return names;
        }

        public static async Task<WLANProfile> GetWiFiProfileAsync(string profileFilename)
        {
            if (!File.Exists(profileFilename))
            {
                Debug.WriteLine($"Error in GetWiFiProfileAsync(). '{profileFilename}' not found.");
                return null;
            }

            string xml = File.ReadAllText(profileFilename);
            var serializer = new XmlSerializer(typeof(WLANProfile));
            using (var reader = new StringReader(xml))
            {
                return (WLANProfile)serializer.Deserialize(reader);
            }

            return null;
        }

        public static async Task<bool> AddWiFiProfileAsync(string profileFilename)
        {
            if (!File.Exists(profileFilename))
            {
                Debug.WriteLine($"Error in AddWiFiProfileAsync(). '{profileFilename}' not found.");
                return false;
            }

            var psi = new ProcessStartInfo()
            {
                FileName = "netsh",
                Arguments = $"wlan add profile filename=\"{profileFilename}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var proc = Process.Start(psi);

            await proc.WaitForExitAsync();
            if (proc.ExitCode != 0)
            {
                Debug.WriteLine($"Error in AddWiFiProfileAsync(). ExitCode={proc.ExitCode}");
                return false;
            }

            return true;
        }

        public static async Task<bool> ConnectWiFiProfileAsync(string profileName)
        {
            var psi = new ProcessStartInfo()
            {
                FileName = "netsh",
                Arguments = $"wlan connect name=\"{profileName}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var proc = Process.Start(psi);

            await proc.WaitForExitAsync();
            if (proc.ExitCode != 0)
            {
                Debug.WriteLine($"Error in ConnectWiFiProfileAsync(). ExitCode={proc.ExitCode}");
                return false;
            }

            return true;
        }
    }
}
